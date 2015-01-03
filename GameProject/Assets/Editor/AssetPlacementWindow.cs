using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


public class AssetPlacementWindow :  EditorWindow {
	
	[MenuItem( "Edit/Asset Placement Window" )]
	static void Init() {
		AssetPlacementWindow window = (AssetPlacementWindow)EditorWindow.GetWindow( typeof( AssetPlacementWindow ) );
		window.maxSize = new Vector2( 300, 200 );
	}
	
	static void RefreshAutoSnap (BoxCollider2D collider2D) {
		if (EditorPrefs.GetBool (AssetPlacement.SnapUpdateKey)) {
			EditorPrefs.SetFloat (AutoGridSnap.MoveSnapXKey, collider2D.size.x);
			EditorPrefs.SetFloat (AutoGridSnap.MoveSnapYKey, collider2D.size.y);
		}
	}
	
	//TODO find a better hotkey
	[MenuItem("Edit/Commands/PlaceAsset #_%_d")]
	static void PlaceAsset() {
		if (AssetPlacementChoiceSystem.selectedAsset != null && AssetPlacementChoiceSystem.selectedAsset.gameObject != null) {
			var placedAsset = PrefabUtility.InstantiatePrefab( AssetPlacementChoiceSystem.selectedAsset.gameObject) as GameObject; 	
			placedAsset.transform.localPosition = AssetPlacementPositionSystem.selectedPosition;
			var collider2D = placedAsset.GetComponent<BoxCollider2D>();
			
			Selection.activeGameObject = placedAsset;
			
			
			
			RefreshAutoSnap (collider2D);
		}
	}	
	
	[MenuItem("Edit/Commands/StartPlaceAsset #_%_e")]
	static void StartPlaceAsset() {
		
		if (AssetPlacementChoiceSystem.instance) {
			Selection.activeGameObject = AssetPlacementChoiceSystem.instance.gameObject;
		}
		
	}	
	
	
	[MenuItem("Edit/Commands/ToggleAutoSnapUpdate #_%_f")]
	static void ToggleAutoSnapUpdate() {
		EditorPrefs.SetBool (AssetPlacement.SnapUpdateKey, !EditorPrefs.GetBool (AssetPlacement.SnapUpdateKey));
	}	
	
	public void OnGUI() {
		EditorPrefs.SetBool(AssetPlacement.SnapUpdateKey, EditorGUILayout.Toggle( "Update Auto Snap", EditorPrefs.GetBool(AssetPlacement.SnapUpdateKey, false)));
	}
}