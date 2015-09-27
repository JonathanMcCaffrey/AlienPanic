
#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.IO;
using UnityEditor;


public class SegmentSerializer : MonoBehaviour {	
	public GameObject selectedNode = null;
	public string segmentName = "Seg1";
	
	public static SegmentSerializer instance = null;
	public SegmentSerializer() {
		instance = this;
	}
	
	
	static string FilePath() { 
		return "Assets/Resources/TextLevelData" + "/";
	}
	
	static void GetOnlyChildren (GameObject root, List<Transform> fixedChildren) {
		var children = root.GetComponentsInChildren<Transform> ();
		
		foreach (var child in children) {
			if (child.transform.parent == root.transform) {
				fixedChildren.Add (child);
			}
		}
	}	
	
	//TODO Make Cleaner
	public static void SaveSelectedNode () {
		if (!instance.selectedNode) {
			return;
		}
		
		var fixedChildren = new List<Transform> ();
		GetOnlyChildren (instance.selectedNode, fixedChildren);
		AssetNodeData rootNode = new AssetNodeData ("PlacementAssets", Vector3.zero);
		foreach (var subRoot in fixedChildren) {
			var subFixedChildren = new List<Transform> ();
			GetOnlyChildren (subRoot.gameObject, subFixedChildren);
			var splitName = subRoot.gameObject.name.Split ('.');
			var node = new AssetNodeData (splitName [2], subRoot.gameObject.transform.localPosition);
			rootNode.children.Add (node);
			foreach (var subChild in subFixedChildren) {
				node.children.Add (new AssetNodeData (subChild.gameObject.name, subChild.gameObject.transform.localPosition));
			}
		}
		XmlSerializer xmlSerializer = new XmlSerializer (typeof(AssetNodeData));
		FileStream file = new FileStream (FilePath () + instance.segmentName + ".txt", FileMode.Create);
		xmlSerializer.Serialize (file, rootNode);
		file.Close ();
	}


	public static GameObject LoadSegmentWithName(string segmentName) {
		XmlSerializer xmlSerializer = new XmlSerializer (typeof(AssetNodeData));
		
		if (!File.Exists (FilePath () + segmentName + ".txt")) {
			return null;
		}
		
		FileStream file = new FileStream (FilePath () + segmentName + ".txt", FileMode.Open);
		
		
		AssetNodeData data = xmlSerializer.Deserialize (file) as AssetNodeData;
		file.Close ();
		String rootName = data.text + " - " + segmentName;
		
		GameObject newSegment = new GameObject (rootName);
		newSegment.transform.localPosition = data.Position ();
		
		Rect segRect = new Rect(int.MaxValue, int.MaxValue, int.MinValue, int.MinValue);
		foreach (var dataNode in data.children) {
			GameObject subLevel = null;
			//subLevel = GameObject.Find (dataNode.text);
			if (!subLevel) {
				subLevel = new GameObject (dataNode.text);
			}
			subLevel.transform.parent = newSegment.transform;
			subLevel.transform.localPosition = dataNode.Position ();
			foreach (var childNode in dataNode.children) {
				//TODO Make cleaner
				string assetString = "Assets/Resources/PlacementAssets/" + dataNode.text + "/" + childNode.text + ".prefab";
				var asset = AssetDatabase.LoadAssetAtPath (assetString, typeof(GameObject)) as GameObject;
				if (asset) {
					var newObject = GameObject.Instantiate (asset) as GameObject;
					newObject.name = childNode.text;
					newObject.transform.parent = subLevel.transform;
					newObject.transform.localPosition = childNode.Position ();
					
					
					//TODO Cleaner
					if(newObject.transform.position.x < segRect.x) {
						segRect.x = newObject.transform.localPosition.x;
					}
					
					if(newObject.transform.position.y < segRect.y) {
						segRect.y = newObject.transform.localPosition.y;
					}
					
					if(newObject.transform.position.x > segRect.width) {
						segRect.width = newObject.transform.localPosition.x;
					}
					
					if(newObject.transform.position.y > segRect.height) {
						segRect.height = newObject.transform.localPosition.y;
					}
				}
			}
		}

		BoxCollider2D colider2D = newSegment.AddComponent<BoxCollider2D> ();
		colider2D.isTrigger = true;
		newSegment.AddComponent<SegmentTriggerVolume> ();

		colider2D.size = new Vector2 (segRect.width - segRect.x, segRect.width - segRect.x);
		colider2D.offset = new Vector2 (colider2D.size.x * 0.5f, colider2D.size.y * 0.5f);

		return newSegment;
	}
	
	public static GameObject LoadNode () {
		return LoadSegmentWithName (instance.segmentName);

	}	
	
	public void Awake() {
		//TODO Move this and improve it
		SegmentManager.AddNewSegement(LoadSegmentWithName (SegmentManager.CreateRandomSegmentName()));
	}
}

[Serializable]
[XmlRoot("node")]
public class AssetNodeData {
	[XmlAttribute("text")]
	public string text = "blank";
	
	[XmlAttribute("x")]
	public float x = 0;
	
	[XmlAttribute("y")]
	public float y = 0;
	
	[XmlAttribute("z")]
	public float z = 0;
	
	public Vector3 Position() {
		return new Vector3 (x, y, z);
	}
	
	[XmlArray("children"),XmlArrayItem("child")]
	public List<AssetNodeData> children = new List<AssetNodeData> ();
	
	public AssetNodeData() {
	}
	
	public AssetNodeData(string text, Vector3 position) {
		this.text = text;
		this.x = position.x;
		this.y = position.y;
		this.z = position.z;
	}
}

#endif
