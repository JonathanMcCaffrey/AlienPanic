using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


//TODO Finish or remove this
public class AssetPlacementWindow :  EditorWindow {
	
	[MenuItem( "Edit/Asset Placement Window" )]
	static void Init() {
		AssetPlacementWindow window = (AssetPlacementWindow)EditorWindow.GetWindow( typeof( AssetPlacementWindow ) );
		window.maxSize = new Vector2( 300, 200 );
	}
	
	//TODO find a better hotkey
	[MenuItem("Edit/Commands/PlaceAsset %_#_d")]
	static void PlaceAsset() {
		Debug.Log ("Pressed");
		
		if (AssetPlacementChoiceSystem.selectedAsset != null && AssetPlacementChoiceSystem.selectedAsset.gameObject != null) {
			var placedAsset = GameObject.Instantiate (AssetPlacementChoiceSystem.selectedAsset.gameObject, AssetPlacementPositionSystem.selectedPosition, Quaternion.identity);	
			
		}
	}		
}