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
	
	
	public List<TabPlacementData> tabList = new List<TabPlacementData>();
	public List<string> tabListRawNames = new List<string>();
	
	//TODO Cleanup tabbing use
	public string selectedTabString = "";
	public int selectedTabNumber = 0;
	
	
	private string folderName = "Resources\\PlacementAssets";
	private string FolderPath() { 		
		return Application.dataPath + "\\" + folderName;
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
		tabListRawNames.Clear ();
		var tabPaths = Directory.GetDirectories (FolderPath ());
		foreach (var filePath in tabPaths) {
			var name = filePath.Remove (0, FolderPath ().Length + 1);
			tabList.Add (new TabPlacementData(filePath, name));
			tabListRawNames.Add (name);
		}
	}
	
	void LoadAssets (){
		//TODO Make a less placeholderish check for this logic. like a reset button thing
		if (assetList.Count == 0) {
			foreach (TabPlacementData tabData in tabList) {
				var filePaths = Directory.GetFiles (tabData.FilePath);
				foreach (string filePath in filePaths) {
					var name = filePath.Remove (0, FolderPath ().Length + 1);
					var localPath = "Assets\\" + filePath.Remove (0, FolderPath ().Length - folderName.Length);
					
					if (name.EndsWith (".prefab")) {
						var assetData = new AssetPlacementData (localPath, name, tabData.name);
						assetList.Add (assetData);
					}
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
		instance = this;
		
		if (once) {
			once = false;
			//TODO Make a resest button
			//assetList.Clear();
			LoadData();
		}
		
		foreach (AssetPlacementData data in assetList) {
			if(data.tab == selectedTabString) {
				if (data.keyCode == (KeyCode)selectedKey) {
					selectedAsset = data;
				}
			}
		}
	}
	
}
