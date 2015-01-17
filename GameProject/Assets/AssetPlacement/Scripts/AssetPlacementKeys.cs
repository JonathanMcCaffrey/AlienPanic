using UnityEngine;
using System.Collections;

public class AssetPlacementKeys {
	//Where the AssetPlacement project is in Assets/
	static public string InstallPath = ""; 

	//Where the AssetPlacement assets is in Assets/
	static public string AssetPathPath = "Resources/"; 
	
	public const string SnapUpdate = "AssetPlacement.doSnapUpdate";
	public static string ShowAll = "AssetPlacement.ShowAll";
	public static string SelectedTab = "AssetPlacement.SelectedTab";
	public static string SelectedKey = "AssetPlacement.SelectedKey";
	public static string SelectedAssetNumber = "AssetPlacement.SelectedAssetNumber";
	
	public static int HotKeySelectionEnabled = -1;
}
