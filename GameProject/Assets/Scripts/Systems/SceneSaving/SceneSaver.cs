
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


public class SceneSaver : MonoBehaviour {	
	public GameObject selectedNode = null;
	public string fileName = "temp";
	
	public static SceneSaver instance = null;
	public SceneSaver() {
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
		FileStream file = new FileStream (FilePath () + instance.fileName + ".txt", FileMode.Create);
		xmlSerializer.Serialize (file, rootNode);
		file.Close ();
	}


	public static GameObject LoadNodeAtPath(string filePath) {
		XmlSerializer xmlSerializer = new XmlSerializer (typeof(AssetNodeData));
		
		if (!File.Exists (FilePath () + filePath + ".txt")) {
			return null;
		}
		
		FileStream file = new FileStream (FilePath () + filePath + ".txt", FileMode.Open);
		
		
		AssetNodeData data = xmlSerializer.Deserialize (file) as AssetNodeData;
		file.Close ();
		GameObject rootLevel = null;
		
		String rootName = data.text + " - " + filePath;
		
		//Lets just make a new one each time instead
		//rootLevel = GameObject.Find (rootName);
		if (!rootLevel) {
			rootLevel = new GameObject (rootName);
		}
		rootLevel.transform.localPosition = data.Position ();
		
		Rect segRect = new Rect(int.MaxValue, int.MaxValue, int.MinValue, int.MinValue);
		foreach (var dataNode in data.children) {
			GameObject subLevel = null;
			//subLevel = GameObject.Find (dataNode.text);
			if (!subLevel) {
				subLevel = new GameObject (dataNode.text);
			}
			subLevel.transform.parent = rootLevel.transform;
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
		
		Segement segData = rootLevel.AddComponent<Segement> ();
		segData.size = new Rect(0, 0, segRect.width - segRect.x, segRect.height - segRect.y);

		BoxCollider2D colider2D = rootLevel.AddComponent<BoxCollider2D> ();
		colider2D.isTrigger = true;

		rootLevel.AddComponent<SegTriggerVolume> ();

		colider2D.size = new Vector2 (segData.size.width, segData.size.height);
		colider2D.offset = new Vector2 (segData.size.width * 0.5f, segData.size.height * 0.5f);

		return rootLevel;
	}
	
	public static GameObject LoadNode () {
		return LoadNodeAtPath (instance.fileName);

	}	
	
	public void Awake() {
		SaveSelectedNode ();
		LoadNode ();
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
