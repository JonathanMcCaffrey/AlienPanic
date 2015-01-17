using UnityEngine;
using System.Collections;

//TODO Rename to AssetPlacmenetGlobals?
public class AssetPlacementKeys {
	//Where the AssetPlacement project is in Assets/
	static public string InstallPath = ""; 

	//Where 'your' assets are in Assets/
	//And assumes they are in a folder called AssetPlacement
	static public string AssetPathPath = "Resources/"; 


	//Internal assets used by AssetPlacementSystem
	//TODO Add all assets here

	//Keys used by AssetPlacementSystem
	public const string SnapUpdate = "AssetPlacement.doSnapUpdate";
	public const string ShowAll = "AssetPlacement.ShowAll";
	public const string SelectedTab = "AssetPlacement.SelectedTab";
	public const string SelectedKey = "AssetPlacement.SelectedKey";
	public const string SelectedAssetNumber = "AssetPlacement.SelectedAssetNumber";

	//Rendering Icon System Keys
	public const string CameraRender3D = "AP.CameraRender3D";
	public const string StageRender3D = "AP.StageRender3D";
	public const string LightMainRender3D = "AP.LightMainRender3D";
	public const string LightSubRender3D = "AP.LightSubRender3D";
	public const string LightSunRender3D = "AP.LightSunRender3D";


	//Constants used by AssetPlacementSystem
	public static int HotKeySelectionEnabled = -1;

}
