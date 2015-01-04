using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


public class AssetPlacementWindow :  EditorWindow {
	//TODO add many many features to this window
	[MenuItem( "Edit/Asset Placement Window" )]
	static void Init() {
		AssetPlacementWindow window = (AssetPlacementWindow)EditorWindow.GetWindow( typeof( AssetPlacementWindow ) );
		window.maxSize = new Vector2( 300, 200 );
	}
	
	static void RefreshAutoSnap (BoxCollider2D snapSize) {
		if (EditorPrefs.GetBool (AssetPlacement.SnapUpdateKey)) {
			EditorPrefs.SetFloat (AutoGridSnap.MoveSnapXKey, snapSize.size.x);
			EditorPrefs.SetFloat (AutoGridSnap.MoveSnapYKey, snapSize.size.y);
		}
	}
	
	static void SetTabContainerParent (GameObject placedAsset) {
		if (AssetPlacementChoiceSystem.instance) {
			if (AssetPlacementChoiceSystem.TabContainerDictionary.ContainsKey (AssetPlacementChoiceSystem.selectedAsset.tab)) {
				var container = AssetPlacementChoiceSystem.TabContainerDictionary [AssetPlacementChoiceSystem.selectedAsset.tab];
				placedAsset.transform.parent = container.transform;
			}
		}
	}
	
	//TODO find a better hotkey
	[MenuItem("Edit/Commands/PlaceAsset #_%_d")]
	static void PlaceAsset() {
		if (AssetPlacementChoiceSystem.selectedAsset != null && AssetPlacementChoiceSystem.selectedAsset.gameObject != null) {
			var placedAsset = PrefabUtility.InstantiatePrefab( AssetPlacementChoiceSystem.selectedAsset.gameObject) as GameObject; 	
			placedAsset.transform.localPosition = AssetPlacementPositionSystem.selectedPosition;
			Selection.activeGameObject = placedAsset;
			
			SetTabContainerParent (placedAsset);
			
			//TODO Use something better than this method, for determing snap size. 
			// Or make the BoxColiders more accurate
			var collider2D = placedAsset.GetComponent<BoxCollider2D>();
			RefreshAutoSnap (collider2D);
		}
	}	
	
	//TODO find a better hotkey
	[MenuItem("Edit/Commands/StartPlaceAsset #_%_e")]
	static void SelectPlaceAssetSystem() {
		if (AssetPlacementChoiceSystem.instance) {
			Selection.activeGameObject = AssetPlacementChoiceSystem.instance.gameObject;
		}
	}	
	
	//TODO find a better hotkey
	[MenuItem("Edit/Commands/ToggleAutoSnapUpdate #_%_f")]
	static void ToggleAutoSnapUpdate() {
		EditorPrefs.SetBool (AssetPlacement.SnapUpdateKey, !EditorPrefs.GetBool (AssetPlacement.SnapUpdateKey));
	}	
	
	public void OnGUI() {
		EditorPrefs.SetBool(AssetPlacement.SnapUpdateKey, EditorGUILayout.Toggle( "Update Auto Snap", EditorPrefs.GetBool(AssetPlacement.SnapUpdateKey, false)));
	}
}