
//Add if using Snazzy Grid
//#define USING_SNAZZY_GRID

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;


public class AssetPlacementWindow :  EditorWindow {
	public static AssetPlacementWindow instance = null;
	
	static void CreateAssetPlacementSystem () {
		string systemName = "AP.AssetPlacementSystem";
		GameObject systemContainer = null;
		systemContainer = GameObject.Find (systemName);
		if (!systemContainer) {
			systemContainer = new GameObject (systemName);
			systemContainer.AddComponent<AssetPlacementChoiceSystem> ();
			var choiceSystem = systemContainer.GetComponent<AssetPlacementChoiceSystem> ();
			choiceSystem.shouldReset = true;
			if (AssetPlacementChoiceSystem.instance) {
				DestroyImmediate (AssetPlacementChoiceSystem.instance.gameObject);
			}
			AssetPlacementChoiceSystem.instance = choiceSystem;
			systemContainer.AddComponent<AssetPlacementPositionSystem> ();
		}
	}
	
	[MenuItem( "Window/Asset Placement Window" )]
	static void Init() {
		if (!instance) {
			AssetPlacementWindow window = (AssetPlacementWindow)EditorWindow.GetWindow (typeof(AssetPlacementWindow));
			window.title = "AP";
			window.minSize = new Vector2 (200, 100);
			instance = window;
			
			CreateAssetPlacementSystem ();
		}
	}
	
	static void RefreshAutoSnap (GameObject placedAsset) {
		#if USING_SNAZZY_GRID
		SnazzyToolsEditor.SnapPos(true, true, true);
		#else
		if (Utils.GameObjectFunctions.HasMesh(placedAsset)) {
			var snapSize = Utils.GameObjectFunctions.CreateRectFromMeshes(placedAsset);
			
			if (EditorPrefs.GetBool (AssetPlacementKeys.SnapUpdate)) {
				EditorPrefs.SetFloat (AutoGridSnap.MoveSnapXKey, snapSize.width);
				EditorPrefs.SetFloat (AutoGridSnap.MoveSnapYKey, snapSize.height);
			}
		}
		#endif
		
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
	[MenuItem("Edit/Commands/Place_Asset #_%_d")]
	static void PlaceAsset() {
		if (AssetPlacementChoiceSystem.selectedAsset != null && AssetPlacementChoiceSystem.selectedAsset.gameObject != null) {
			var placedAsset = PrefabUtility.InstantiatePrefab(AssetPlacementChoiceSystem.selectedAsset.gameObject) as GameObject; 	
			placedAsset.transform.localPosition = AssetPlacementPositionSystem.selectedPosition;
			Selection.activeGameObject = placedAsset;
			
			SetTabContainerParent (placedAsset);
			RefreshAutoSnap (placedAsset);
		}
	}	
	
	//TODO find a better hotkey
	[MenuItem("Edit/Commands/Select_PlaceAsset #_%_e")]
	static void SelectPlaceAssetSystem() {
		if (AssetPlacementChoiceSystem.instance) {
			Selection.activeGameObject = AssetPlacementChoiceSystem.instance.gameObject;
		}
	}	
	
	//TODO find a better hotkey
	#if !USING_SNAZZY_GRID
	[MenuItem("Edit/Commands/ToggleAutoSnapUpdate #_%_f")]
	#endif
	static void ToggleAutoSnapUpdate() {
		EditorPrefs.SetBool (AssetPlacementKeys.SnapUpdate, !EditorPrefs.GetBool (AssetPlacementKeys.SnapUpdate));
	}	
	
	void OnInspectorUpdate() {
		if (background == null) {
			Load();
		}
	}
	
	private Texture background = null;
	private Texture backgroundAlpha = null;
	private Texture windowTitle = null;
	
	//TODO Create some kinda of do once function
	bool warnOnce = true;
	void Load() {
		string path = "Assets/"+AssetPlacementKeys.InstallPath+"AssetPlacement/Resources/GUI/";
		background = AssetDatabase.LoadAssetAtPath(path+"BG.jpg",typeof(Texture)) as Texture;
		if (!background && warnOnce) {
			warnOnce = false;
			Debug.Log ("AssetPlacement InstallPath Needs Fixing");
		}
		
		backgroundAlpha = AssetDatabase.LoadAssetAtPath(path+"BGAlpha.png",typeof(Texture)) as Texture;
		windowTitle = AssetDatabase.LoadAssetAtPath(path+"Title.jpg",typeof(Texture)) as Texture;
	}
	
	void CreateTitleLogo (float width, ref float distanceFromTop) {
		EditorGUI.LabelField (new Rect (0, 0, width, 64), new GUIContent (windowTitle, "//TODO Add Tooltip"));
		distanceFromTop += 64;
	}
	
	static void CreateAutoSnapToggle (float width, ref float distanceFromTop) {
		EditorPrefs.SetBool (
			AssetPlacementKeys.SnapUpdate,
			EditorGUI.Toggle (
			new Rect (-1, distanceFromTop, width, 20),
			"Update Auto Snap",
			EditorPrefs.GetBool (AssetPlacementKeys.SnapUpdate, false)));
		
		distanceFromTop += 20;
	}
	
	void CreateShowLabelsToggle (float width, ref float distanceFromTop) {
		float toggleHeight = 16;
		var shouldShowLabels = EditorPrefs.GetBool (AssetPlacementKeys.ShowLabels);
		shouldShowLabels = EditorGUI.Toggle (new Rect (0, distanceFromTop, width, toggleHeight), "Show Labels", shouldShowLabels);
		EditorPrefs.SetBool (AssetPlacementKeys.ShowLabels, shouldShowLabels);
		
		distanceFromTop += toggleHeight;
	}
	
	void CreateToggleTabSelection (float width, ref float distanceFromTop) {
		float toggleHeight = 16;
		
		var shouldShowAll = EditorPrefs.GetBool (AssetPlacementKeys.ShowAll);
		shouldShowAll = EditorGUI.Toggle (new Rect (0, distanceFromTop, width, toggleHeight), "Show All", shouldShowAll);
		EditorPrefs.SetBool (AssetPlacementKeys.ShowAll, shouldShowAll);
		
		distanceFromTop += toggleHeight;
		if (!shouldShowAll) {
			float popupHeight = 20;
			int selectedTabNumber = EditorPrefs.GetInt (AssetPlacementKeys.SelectedTab);
			selectedTabNumber = EditorGUI.Popup (new Rect (0, distanceFromTop, width, popupHeight), selectedTabNumber, AssetPlacementChoiceSystem.instance.tabNames.ToArray ());
			EditorPrefs.SetInt (AssetPlacementKeys.SelectedTab, selectedTabNumber);
			distanceFromTop += popupHeight;
			
			if (AssetPlacementChoiceSystem.instance.tabList.Count < selectedTabNumber) {
				AssetPlacementChoiceSystem.instance.selectedTab = AssetPlacementChoiceSystem.instance.tabList [selectedTabNumber];
			}
		} else {
			distanceFromTop += 8;
		}
	}
	
	void CreateHotkeyLabel (Rect buttonRect, string keyLabel) {
		Rect labelRect = new Rect (buttonRect.x + buttonRect.width * 0.7f, buttonRect.y + buttonRect.height * 0.7f, buttonRect.width * 0.3f, buttonRect.width * 0.3f);
		
		GUI.DrawTexture (new Rect (labelRect.x - labelRect.width * 0.1f,
		                           labelRect.y - labelRect.height * 0.1f, 
		                           labelRect.width,
		                           labelRect.height)
		                 , backgroundAlpha);
		
		GUIStyle labelStyle = new GUIStyle ();
		labelStyle.fontSize = 18;
		labelStyle.fontStyle = FontStyle.Bold;
		labelStyle.normal.textColor = Color.black;
		GUI.Label (labelRect, keyLabel, labelStyle);
	}
	
	void CreateTabLabel (AssetPlacementData assetData, Rect buttonRect) {
		Rect labelRect = new Rect(buttonRect.x + buttonRect.width * 0.1f, buttonRect.y + buttonRect.height * 0.1f, buttonRect.width * 0.8f, buttonRect.width * 0.2f); 
		
		GUI.DrawTexture (new Rect (labelRect.x,
		                           labelRect.y - labelRect.height * 0.1f, 
		                           labelRect.width,
		                           labelRect.height)
		                 , backgroundAlpha);
		
		var labelStyle = new GUIStyle ();
		labelStyle.fontSize = 14;
		labelStyle.fontStyle = FontStyle.Bold;
		labelStyle.normal.textColor = Color.black;
		GUI.Label (labelRect, assetData.tab, labelStyle);
	}
	
	bool hasMadeAnIconRenderAsset = false;
	void CreateAssetButtons (float width, ref float distanceFromTop) {
		var shouldShowAll = EditorPrefs.GetBool (AssetPlacementKeys.ShowAll);
		var shouldShowLabels = EditorPrefs.GetBool (AssetPlacementKeys.ShowLabels);
		
		int assetIndex = 0;
		float xCoord = 0;
		float yCoord = 0;
		
		foreach (var assetData in AssetPlacementChoiceSystem.instance.assetList) {
			if (assetData.tab != AssetPlacementChoiceSystem.instance.selectedTab.name && !shouldShowAll) {
				assetIndex++;
				continue;
			}
			
			if(assetData.gameObject == null) { continue; }
			
			Texture2D usedTexture = null;
			if (assetData.gameObject.GetComponent<SpriteRenderer> () || 
			    assetData.gameObject.GetComponentsInChildren<SpriteRenderer>().Length > 0) {
				
				usedTexture = assetData.gameObject.GetComponent<SpriteRenderer> ().sprite.texture;
			} 
			//TODO This isn't working for some reason. Figure out why
			else if (true ||assetData.gameObject.GetComponent<MeshRenderer> () || 
			         assetData.gameObject.GetComponentsInChildren<MeshRenderer>().Length > 0) {
				
				var tempTexture = AssetPlacementIconRenderer.CreateTextureFromCamera(assetData, ref hasMadeAnIconRenderAsset);
				usedTexture = tempTexture; 
			} else {
				//TODO Add other edge cases
			}
			
			var buttonStyle = EditorPrefs.GetInt (AssetPlacementKeys.SelectedAssetNumber) == assetIndex ? GUI.skin.box : GUI.skin.button;
			
			var buttonRect = new Rect ((width / 3.0f) * xCoord, distanceFromTop + (width / 3.0f) * yCoord, (width / 3.0f), (width / 3.0f));
			if (usedTexture && GUI.Button (buttonRect, usedTexture, buttonStyle)) {
				EditorPrefs.SetInt (AssetPlacementKeys.SelectedAssetNumber, assetIndex);
			}
			
			string keyLabel = assetData.keyCode.ToString();
			if(keyLabel == "None") { keyLabel = "[]"; } 
			else if(keyLabel.Length > 1) { keyLabel = keyLabel.Remove(0,keyLabel.Length - 1); }
			
			if(shouldShowLabels) {
				if(assetData.tab == AssetPlacementChoiceSystem.instance.selectedTab.name) {
					CreateHotkeyLabel (buttonRect, keyLabel);
				}
				if(shouldShowAll) {
					CreateTabLabel (assetData, buttonRect);
				}
			}
			
			assetIndex++; xCoord++;
			if (xCoord > 2) { xCoord = 0; yCoord++; }
		}
		
		if (hasMadeAnIconRenderAsset) {
			AssetPlacementIconRenderer.CleanUpRender3DAssets();
		}
	}
	
	Vector2 scrollPosition = Vector2.zero;
	void CreateAssetListScrollView (float width, ref float distanceFromTop) {
		if (AssetPlacementChoiceSystem.instance.selectedTab == null) {
			return;
		}
		
		float dist = distanceFromTop;
		int assetCount = 0;
		foreach (var assetData in AssetPlacementChoiceSystem.instance.assetList) {
			var shouldShowAll = EditorPrefs.GetBool (AssetPlacementKeys.ShowAll);
			if (assetData.tab != AssetPlacementChoiceSystem.instance.selectedTab.name && !shouldShowAll) {
				continue;
			}
			assetCount++;
		}
		float scrollMax = (((assetCount / 3) + 1.0f) * width / 3.0f) + 2.0f;
		float viewMax = Screen.height - dist - 5;
		float buffer = scrollMax > viewMax ? 12 : 0;
		scrollPosition = GUI.BeginScrollView (new Rect (0.0f, dist, width, viewMax), scrollPosition, new Rect (0.0f, dist, width - buffer, scrollMax));
		CreateAssetButtons (width - buffer, ref distanceFromTop);
		GUI.EndScrollView ();
	}
	
	static GUIStyle CreateWarningFontStyle () {
		var angryFont = new GUIStyle ();
		angryFont.normal.textColor = Color.red;
		angryFont.fontStyle = FontStyle.Bold;
		return angryFont;
	}
	
	public void OnGUI() {
		instance = this;
		
		if (AssetPlacementChoiceSystem.instance == null) {
			GUI.Label (new Rect(0, 0, Screen.width, 120), "[Error]\nSystem Was Deleted\nPlease Refresh Window\nTo Keep Using", CreateWarningFontStyle ());
			return;
		}
		
		if (background) {
			float width = Screen.width;
			EditorGUI.DrawPreviewTexture (new Rect (0, 0, Screen.width, Screen.height), background);
			
			float distanceFromTop = 0.0f;
			
			CreateTitleLogo (width, ref distanceFromTop);
			
			if(AssetPlacementChoiceSystem.instance.tabList == null || AssetPlacementChoiceSystem.instance.tabList.Count == 0) {
				GUI.Label (new Rect(0, distanceFromTop, width, 20), "No Assets Found", CreateWarningFontStyle ());
				return;
			}
			
			CreateAutoSnapToggle (width, ref distanceFromTop);
			CreateShowLabelsToggle (width, ref distanceFromTop);
			CreateToggleTabSelection (width, ref distanceFromTop);
			CreateAssetListScrollView (width, ref distanceFromTop);
		}
	}
}