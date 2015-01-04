using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(AssetPlacementChoiceSystem))]
[CanEditMultipleObjects]
public class AssetPlacementChoiceSystemUnity : Editor {	
	SerializedProperty assetList;
	SerializedProperty selectedKey;
	
	//TODO Use tab data instead of split data
	SerializedProperty selectedTabString;
	SerializedProperty selectedTabNumber;
	
	//TODO Do something as with asset list, and use tabs instead of raw names
	SerializedProperty tabListRawNames;
	
	int keyValue = -1;
	
	void OnEnable() {
		assetList = serializedObject.FindProperty ("assetList");
		selectedKey = serializedObject.FindProperty ("selectedKey");
		
		selectedTabString = serializedObject.FindProperty ("selectedTabString");
		selectedTabNumber = serializedObject.FindProperty ("selectedTabNumber");
		
		tabListRawNames = serializedObject.FindProperty ("tabListRawNames");
	}
	
	void CreateTabSelection () {
		List<string> extractedTabNameList = new List<string> ();
		for (int index = 0; index < tabListRawNames.arraySize; index++) {
			extractedTabNameList.Add (tabListRawNames.GetArrayElementAtIndex (index).stringValue);
		}
		
		if (extractedTabNameList.Count > 0) {
			selectedTabNumber.intValue = GUILayout.SelectionGrid (selectedTabNumber.intValue, extractedTabNameList.ToArray(), extractedTabNameList.Count);
			selectedTabString.stringValue = extractedTabNameList [selectedTabNumber.intValue];
		}
	}
	
	void CreateAssetSelection () {
		for (int index = 0; index < assetList.arraySize; index++) {
			var tabName = assetList.GetArrayElementAtIndex (index).FindPropertyRelative("tab").stringValue;
			
			if(string.Compare(tabName, selectedTabString.stringValue) == 0) {
				EditorGUILayout.BeginVertical ();
				EditorGUILayout.PropertyField (assetList.GetArrayElementAtIndex (index), true);
				
				if(assetList.GetArrayElementAtIndex (index).FindPropertyRelative("gameObject").objectReferenceValue == null) {
					string fixedPath = assetList.GetArrayElementAtIndex (index).FindPropertyRelative("filePath").stringValue; 
					fixedPath = fixedPath.Replace('\\', '/');
					
					var prefab = AssetDatabase.LoadAssetAtPath(fixedPath, typeof(GameObject)) as GameObject;
					assetList.GetArrayElementAtIndex (index).FindPropertyRelative("gameObject").objectReferenceValue =  prefab;
				}
				
				EditorGUILayout.EndVertical ();
			}
		}
	}
	
	void UpdateSelectedKey () {
		if (keyValue != -1) {
			selectedKey.intValue = keyValue;
		}
	}
	
	public override void OnInspectorGUI() {
		serializedObject.Update ();
		
		GUILayout.Label ("Asset Count: " + assetList.arraySize.ToString ());
		GUILayout.Label ("Selected Key: " +((KeyCode)selectedKey.intValue).ToString ());
		
		CreateTabSelection ();
		CreateAssetSelection ();
		
		UpdateSelectedKey ();
		
		serializedObject.ApplyModifiedProperties ();
	}
	
	public void OnSceneGUI() {
		//TODO Tabbing
		if (Event.current.keyCode != KeyCode.None) {
			keyValue  = (int)Event.current.keyCode;
		}
	}
}

