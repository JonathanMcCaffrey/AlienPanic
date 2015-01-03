using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(AssetPlacementChoiceSystem))]
[CanEditMultipleObjects]
public class AssetPlacementChoiceSystemUnity : Editor {
	
	SerializedProperty assetList;
	SerializedProperty selectedKey;
	
	int keyValue = -1;
	
	void OnEnable() {
		assetList = serializedObject.FindProperty ("assetList");
		selectedKey = serializedObject.FindProperty ("selectedKey");
	}
	
	public override void OnInspectorGUI() {
		serializedObject.Update ();
		
		GUILayout.Label ("Asset Count: " + assetList.arraySize.ToString ());
		
		GUILayout.Label ("Selected Key: " +((KeyCode)selectedKey.intValue).ToString ());
		
		
		for (int index = 0; index < assetList.arraySize; index++) {
			EditorGUILayout.BeginVertical();
			EditorGUILayout.PropertyField (assetList.GetArrayElementAtIndex(index), true);
			EditorGUILayout.EndVertical();	
		}
		
		if (keyValue != -1) {
			selectedKey.intValue = keyValue;
			
		}
		
		serializedObject.ApplyModifiedProperties ();
	}
	
	public void OnSceneGUI() {
		//TODO Tabbing
		if (Event.current.keyCode != KeyCode.None) {
			keyValue  = (int)Event.current.keyCode;
		}
	}
}

