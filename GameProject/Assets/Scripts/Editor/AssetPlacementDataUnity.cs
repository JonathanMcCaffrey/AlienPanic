using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomPropertyDrawer(typeof(AssetPlacementData))]
public class AssetPlacementDataUnity : PropertyDrawer {
	SerializedProperty gameObject = null;
	SerializedProperty keyCode = null;
	
	
	public override void OnGUI(Rect rect, SerializedProperty prop, GUIContent label) {
		SerializedProperty gameObject = prop.FindPropertyRelative ("gameObject");
		SerializedProperty keyCode = prop.FindPropertyRelative ("keyCode");
		SerializedProperty name = prop.FindPropertyRelative ("name");
		
		/*
		GUI.Label (new Rect (rect.x, rect.y, rect.width / 3, rect.height), name.stringValue);
		EditorGUI.PropertyField (new Rect(rect.x + rect.width / 3, rect.y, rect.width / 3, rect.height), keyCode);
		EditorGUI.PropertyField (new Rect(rect.x + rect.width / 1.5f, rect.y, rect.width / 3, rect.height), gameObject);
		*/
		
		GUILayout.Label (name.stringValue);
		EditorGUILayout.PropertyField (keyCode);
		EditorGUILayout.PropertyField (gameObject);
		
	}
	
	
	
}

