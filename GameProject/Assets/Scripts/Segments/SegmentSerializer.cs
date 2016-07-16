/*
 * 	SegmentSerializer.cs
 * 	
 * Allows for the saving of Segments to be used in the SegmentManager.cs
 * 
 * Use the Asset placement system to create a Segment. With current defaults, create a level starting a 0,0 in world space
 * Then go Right and Up in building the Segment. (Segments orgins are currently expected as 0,0)
 * 
 * Suggest using the Camera Quadrants as a sizing guide.
 * 
 * When happy with Segment don't forget to save it with a unique name!
 * 	
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.IO;

public class SegmentSerializer : MonoBehaviour {	
	public GameObject selectedNode = null;
	public string segmentName = "Seg1";
	
	public static SegmentSerializer instance = null;
	public SegmentSerializer() {
		instance = this;
	}

	static string FilePath() { 
		return "Assets/Resources/TextLevelData/Segments/";
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
		//XmlSerializer xmlSerializer = new XmlSerializer (typeof(AssetNodeData));
		
		//if (!File.Exists (FilePath () + segmentName + ".txt")) {
		//	return null; //This code does not work on Mobile
		//}
		
		//FileStream file = new FileStream (FilePath () + segmentName + ".txt", FileMode.Open);
		
		TextAsset file = Resources.Load<TextAsset>("TextLevelData/Segments/" + segmentName) as TextAsset;
		
		
		AssetNodeData data = new AssetNodeData (file.text); 
		//	AssetNodeData data = xmlSerializer.Deserialize (file) as AssetNodeData;
		
		
		//file.Close ();
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
				
				
				var asset = Resources.Load("PlacementAssets/" + dataNode.text + "/" + childNode.text) as GameObject;
				
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
		SegmentManager.spawnedSegCount = 0;
		SegmentManager.AddNewSegement(LoadSegmentWithName (SegmentManager.CreateRandomSegmentName()));
	}
}

[Serializable]
[XmlRoot("node")] //TODO Make this its own file class
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

	//This is a Mobile port of the XML Serializer.
	//Don't worry about it when just using the editor.
	public AssetNodeData(string data) {
		
		XmlTextReader reader = new XmlTextReader(new StringReader(data));

		List<AssetNodeData> subChildren = new List<AssetNodeData> ();

		while(reader.Read())
		{
			if(reader.IsStartElement("child")) {
				String text = reader.GetAttribute ("text");

				String inner = reader.ReadInnerXml();

				AssetNodeData node = new AssetNodeData();
				node.initSubs(inner);
				node.text = text;
				children.Add(node);
			}
		}
		
	}

	//This is probably hacky code. Ensure it makes sense, or rewrite
	public void initSubs(string data) {
		
		XmlTextReader reader = new XmlTextReader(new StringReader(data));
		
		int count = 0;
		while (reader.Read()) {
			
			
			if (reader.GetAttribute ("text") != null && !reader.GetAttribute ("text").Equals ("")) {
				count++;
				
				AssetNodeData node = new AssetNodeData ();
				children.Add (node);
				children [count - 1].text = reader.GetAttribute ("text");
			}
			
			
			if (reader.GetAttribute ("x") != null && !reader.GetAttribute ("x").Equals ("")) {
				children [count - 1].x = float.Parse (reader.GetAttribute ("x"));
			}

			if (reader.GetAttribute ("y") != null && !reader.GetAttribute ("y").Equals ("")) {
				children [count - 1].y = float.Parse (reader.GetAttribute ("y"));
			}
			
			
			if (reader.GetAttribute ("z") != null && !reader.GetAttribute ("z").Equals ("")) {
				children [count - 1].z = float.Parse (reader.GetAttribute ("z"));
			}
		}
	}
	
	public AssetNodeData() {
		
	}
	
	public AssetNodeData(string text, Vector3 position) {
		this.text = text;
		this.x = position.x;
		this.y = position.y;
		this.z = position.z;
	}
}

