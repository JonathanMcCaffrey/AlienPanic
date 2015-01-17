
//Add if using Snazzy Grid
//#define USING_SNAZZY_GRID

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;


public class AssetPlacementWindow :  EditorWindow {
	//TODO Save these
	bool shouldShowAll = false;
	bool shouldShowLabels = false;
	
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
	//TODO Re-add later
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
		//TODO Check this sometime to make sure it still works
		//TODO Maybe start using TDD? 
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
		shouldShowLabels = EditorGUI.Toggle (new Rect (0, distanceFromTop, width, toggleHeight), "Show Labels", shouldShowLabels);
		distanceFromTop += toggleHeight;
	}
	
	void CreateToggleTabSelection (float width, ref float distanceFromTop) {
		float toggleHeight = 16;
		shouldShowAll = EditorGUI.Toggle (new Rect (0, distanceFromTop, width, toggleHeight), "Show All", shouldShowAll);
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
	
	
	private int textureWidth = 128; 
	private int textureHeight = 128;
	
	//TODO Add a retake photos button? Generic Move camera controls?
	
	//TODO Do something better than this
	Vector3 cameraPosition = new Vector3 (-5.09f, 16.97f, -7.5f);
	Vector3 cameraRotation = new Vector3 (39.80953f, 44.82499f, -14.40204f);
	
	
	//TODO Make this a seperate class
	Texture2D CreateTextureFromCamera(AssetPlacementData assetData) {
		string fixedName = assetData.name.Replace('\\', '_'); 
		fixedName = fixedName.Replace('/', '_');
		fixedName = fixedName.Replace('.', '_');
		
		var directoryPath = Application.dataPath + "/Resources/PlacementIcons/";
		string textureFilePath = directoryPath + fixedName + ".png";
		if (!Directory.Exists (directoryPath)) {
			Directory.CreateDirectory(directoryPath);
		}
		
		var resourcePath = "PlacementIcons/" + fixedName;
		if (File.Exists (textureFilePath)) {
			var texture = Resources.Load<Texture2D>(resourcePath);
			return texture;
		}
		
		//TODO Staged asset code below. Refactor
		
		//TODO Refactor CreateCamera
		string cameraName = AssetPlacementKeys.CameraRender3D;
		GameObject cameraContainer = null;
		cameraContainer = GameObject.Find (cameraName);
		
		Camera stagedCamera = null;
		if (!cameraContainer) {
			cameraContainer = new GameObject(cameraName);
			stagedCamera = cameraContainer.AddComponent<Camera>();
			stagedCamera.transform.localPosition = cameraPosition;
			stagedCamera.transform.Rotate(cameraRotation);
			
			
			
			
			//TODO Move and aim camera
		} else {
			stagedCamera = cameraContainer.GetComponent<Camera> ();
		}
		
		//TODO Refactor CreateStage
		
		
		//TODO Delete stage and camera after all screenshots have been taken
		string stagedName = AssetPlacementKeys.StageRender3D;
		GameObject stagedContainer = null;
		stagedContainer = GameObject.Find (stagedName);
		
		if (!stagedContainer) {
			stagedContainer = new GameObject(stagedName);
		}
		
		
		//TODO Create Stage Lights
		
		//TODO Move all this vars to another files
		string stagedLightMain = AssetPlacementKeys.LightMainRender3D;
		GameObject stagedLightMainContainer = null;
		stagedLightMainContainer = GameObject.Find (stagedLightMain);
		
		if (!stagedLightMainContainer) {
			stagedLightMainContainer = new GameObject(stagedLightMain);
			stagedLightMainContainer.AddComponent<Light>();
			
			stagedLightMainContainer.transform.position = new Vector3(15.57f, 6.45f, 0.75f);
			var light = stagedLightMainContainer.GetComponent<Light>();
			light.type = LightType.Point;
			light.range = 21;
			light.intensity = 6.8f;
		}
		
		string stagedLightSub = AssetPlacementKeys.LightSubRender3D;
		GameObject stagedLightSubContainer = null;
		stagedLightSubContainer = GameObject.Find (stagedLightSub);
		
		if (!stagedLightSubContainer) {
			stagedLightSubContainer = new GameObject(stagedLightSub);
			stagedLightSubContainer.AddComponent<Light>();
			
			stagedLightSubContainer.transform.position = new Vector3(-3.28f, -1.52f, 1.87f);
			var light = stagedLightSubContainer.GetComponent<Light>();
			light.type = LightType.Point;
			light.range = 205.2f;
			light.intensity = 0.3f;
			light.color = Color.gray;
		}
		
		
		string stagedLightSun = AssetPlacementKeys.LightSunRender3D;
		GameObject stageLightSunContainer = null;
		stageLightSunContainer = GameObject.Find (stagedLightSun);
		
		if (!stageLightSunContainer) {
			stageLightSunContainer = new GameObject(stagedLightSun);
			stageLightSunContainer.AddComponent<Light>();
			
			stageLightSunContainer.transform.position = new Vector3(-2.35f, 5.54f, 15.01f);
			stageLightSunContainer.transform.Rotate(new Vector3(49.5479f, 20.51822f, 120.4922f));
			
			var light = stageLightSunContainer.GetComponent<Light>();
			light.type = LightType.Directional;
			light.intensity = 0.2f;
			light.color = Color.white;
		}
		
		
		hasMadeAnIconRenderAsset = true;
		
		
		
		//TODO Refactor: seems too long
		
		//TODO Refactor: CreatingAssetToScreenshot
		var stagedAsset = PrefabUtility.InstantiatePrefab(assetData.gameObject) as GameObject; 	
		stagedAsset.name = "StagedAsset";
		DontDestroyOnLoad (stagedAsset);
		stagedAsset.transform.parent = stagedContainer.transform;
		stagedAsset.transform.localPosition = Vector3.zero;
		SceneView.RepaintAll ();
		
		//TODO Refactor: CreateIconFromStage
		RenderTexture rt = new RenderTexture(textureWidth, textureHeight, 0, RenderTextureFormat.ARGB32);
		RenderTexture.active = rt;
		stagedCamera.targetTexture = rt;
		
		//TODO Format this text better
		Texture2D screenShot = new Texture2D (textureWidth, textureHeight, TextureFormat.ARGB32, false);
		stagedCamera.Render ();
		
		screenShot.ReadPixels (new Rect (0, 0, textureWidth, textureHeight), 0, 0);
		
		var bytes = screenShot.EncodeToJPG ();
		
		RenderTexture.active = null;
		DestroyImmediate (screenShot);
		
		File.WriteAllBytes (textureFilePath, bytes);
		
		stagedCamera.targetTexture = null;
		RenderTexture.active = null; 
		
		DestroyImmediate (stagedAsset);
		
		return null;
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
		int index = 0;
		float xVal = 0;
		float yVal = 0;
		
		foreach (var assetData in AssetPlacementChoiceSystem.instance.assetList) {
			if (assetData.tab != AssetPlacementChoiceSystem.instance.selectedTab.name && !shouldShowAll) {
				index++;
				continue;
			}
			
			if(assetData.gameObject == null) {
				continue;
			}
			
			Texture2D usedTexture = null;
			if (assetData.gameObject.GetComponent<SpriteRenderer> () || 
			    assetData.gameObject.GetComponentsInChildren<SpriteRenderer>().Length > 0) {
				
				usedTexture = assetData.gameObject.GetComponent<SpriteRenderer> ().sprite.texture;
			} 
			//TODO This isn't working for some reason. Figure out why
			else if (true ||assetData.gameObject.GetComponent<MeshRenderer> () || 
			         assetData.gameObject.GetComponentsInChildren<MeshRenderer>().Length > 0) {
				
				var tempTexture = CreateTextureFromCamera(assetData);
				if(tempTexture) {
					usedTexture = tempTexture;
				}
			} else {
				//TODO Add other edge cases
			}
			
			var buttonStyle = EditorPrefs.GetInt (AssetPlacementKeys.SelectedAssetNumber) == index ? GUI.skin.box : GUI.skin.button;
			
			//TODO Add all hotkeys setup and show duplicate keys, 
			// or visually indicate which hotkeys are currently working
			
			var buttonRect = new Rect ((width / 3.0f) * xVal, distanceFromTop + (width / 3.0f) * yVal, (width / 3.0f), (width / 3.0f));
			if (usedTexture && GUI.Button (buttonRect, usedTexture, buttonStyle)) {
				EditorPrefs.SetInt (AssetPlacementKeys.SelectedAssetNumber, index);
			}
			
			//TODO Consider none key. Currently shows 'e' for it, 'e' is not none
			string keyLabel = assetData.keyCode.ToString();
			if(keyLabel.Length > 1) {
				keyLabel = keyLabel.Remove(0,keyLabel.Length - 1); 
			}
			
			if(shouldShowLabels) {
				CreateHotkeyLabel (buttonRect, keyLabel);
				if(shouldShowAll) {
					CreateTabLabel (assetData, buttonRect);
				}
			}
			
			index++;
			xVal++;
			if (xVal > 2) {
				xVal = 0;
				yVal++;
			}
		}
		
		if (hasMadeAnIconRenderAsset) {
			CleanUpRender3DAssets ();	
		}
	}
	
	void CleanUpRender3DAssets () {
		var temp = GameObject.Find (AssetPlacementKeys.CameraRender3D);
		if (temp) DestroyImmediate (temp);
		temp = GameObject.Find (AssetPlacementKeys.StageRender3D);
		if (temp) DestroyImmediate (temp);
		temp = GameObject.Find (AssetPlacementKeys.LightMainRender3D);
		if (temp) DestroyImmediate (temp);
		temp = GameObject.Find (AssetPlacementKeys.LightSubRender3D);
		if (temp) DestroyImmediate (temp);
		temp = GameObject.Find (AssetPlacementKeys.LightSunRender3D);
		if (temp) DestroyImmediate (temp);
	}
	
	Vector2 scrollPosition = Vector2.zero;
	
	void CreateAssetListScrollView (float width, ref float distanceFromTop) {
		if (AssetPlacementChoiceSystem.instance.selectedTab == null) {
			//TODO No tab selected. Auto select one or something
			return;
		}
		
		
		float dist = distanceFromTop;
		int assetCount = 0;
		foreach (var assetData in AssetPlacementChoiceSystem.instance.assetList) {
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
	
	public void OnGUI() {
		instance = this;
		
		if (AssetPlacementChoiceSystem.instance == null) {
			return;
		}
		
		if (background) {
			float width = Screen.width;
			
			EditorGUI.DrawPreviewTexture (new Rect (0, 0, Screen.width, Screen.height), background);
			
			float distanceFromTop = 0.0f;
			
			CreateTitleLogo (width, ref distanceFromTop);
			
			if(AssetPlacementChoiceSystem.instance.tabList == null || AssetPlacementChoiceSystem.instance.tabList.Count == 0) {
				//TODO Make this font red and bold
				GUI.Label (new Rect(0, distanceFromTop, width, 20), "No Assets Found");
				return;
			}
			
			CreateAutoSnapToggle (width, ref distanceFromTop);
			CreateShowLabelsToggle (width, ref distanceFromTop);
			CreateToggleTabSelection (width, ref distanceFromTop);
			
			CreateAssetListScrollView (width, ref distanceFromTop);
		}
	}
}