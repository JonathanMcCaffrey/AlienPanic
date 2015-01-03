using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO Wrap this since it will probably break mobile
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;


public class AssetPlacementChoiceSystem : MonoBehaviour {
	public enum GridSpacing { Vertical, Horizontal }
	
	public bool autoSetGrid = true;
	private GridSpacing gridStack = GridSpacing.Horizontal;
	
	public int selectedKey = (int)KeyCode.None;
	public static AssetPlacementData selectedAsset = null; 
	
	public List<AssetPlacementData> assetList = new List<AssetPlacementData>(); 
	public List<AssetPlacementData> AssetList {
		get { return assetList; }
	}
	
	//TODO Adding the tabbing interface
	private List<TabPlacementData> tabList = new List<TabPlacementData>();
	public List<TabPlacementData> TabList {
		get { return tabList; }
	}
	
	private string folderName = "Resources/PlacementAssets";
	private string FolderPath() { 		
		return Application.dataPath + "/" + folderName;
	}
	
	public static AssetPlacementChoiceSystem instance = null;
	void Awake() {
		if (instance && instance != this) {
			Destroy (gameObject);
		} else {
			DontDestroyOnLoad(gameObject);
			instance = this;
		}
	}
	
	void LoadTabs () {
		var tabPaths = Directory.GetDirectories (FolderPath ());
		foreach (var filePath in tabPaths) {
			var name = filePath.Remove (0, FolderPath ().Length + 1);
			tabList.Add (new TabPlacementData(filePath, name));
		}
	}
	
	void LoadAssets (){
		//TODO Make a less placeholderish check for this logic. like a reset button thing
		if (assetList.Count == 0) {
			foreach (TabPlacementData tabData in tabList) {
				var filePaths = Directory.GetFiles (tabData.FilePath);
				foreach (string filePath in filePaths) {
					var name = filePath.Remove (0, FolderPath ().Length + 1);
					if (name.EndsWith (".prefab")) {
						var assetData = new AssetPlacementData (filePath, name, tabData.name);
						assetList.Add (assetData);
						
						/* //TODO Find some way to dynamically set this (gameObject)
						var path = "PlacementAssets\\" + name;
						path = path.Remove(path.Length - 7,7); 
						assetData.gameObject = Resources.Load<GameObject>(path) as GameObject;
						if(assetData.gameObject) {Debug.Log("Worked");}
						
						Debug.Log(path);
						*/}
				}
			}
		}
	}
	
	private void LoadData() {
		if (!Directory.Exists (FolderPath ())) {
			Directory.CreateDirectory (FolderPath ());
		}
		
		LoadTabs ();
		LoadAssets ();
	}
	
	bool once = true;
	public void OnDrawGizmos() {
		if (once) {
			instance = this;
			once = false;
			//TODO Make this a button
			//assetList.Clear();
			LoadData();
		}
		
		//TODO Tabbing
		foreach (AssetPlacementData data in assetList) {
			
			Debug.Log("This Code: " +data.keyCode);
			Debug.Log("Other Code: " +(KeyCode)selectedKey);
			
			if (data.keyCode == (KeyCode)selectedKey) {
				Debug.Log (data.name);
				selectedAsset = data;
			}
		}
	}
	
}
