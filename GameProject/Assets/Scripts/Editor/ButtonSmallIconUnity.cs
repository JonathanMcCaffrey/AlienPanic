using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ButtonSmallIcon))]
[CanEditMultipleObjects]
public class ButtonSmallIconUnity : Editor {
	
	SerializedProperty disableModifiedControls;
	
	SerializedProperty icon;
	SerializedProperty background;
	SerializedProperty button;
	
	SerializedProperty iconSprite;
	SerializedProperty backgroundColor;
	SerializedProperty onButtonClick;
	
	
	void OnEnable() {
		disableModifiedControls = serializedObject.FindProperty ("disableModifiedControls");
		
		icon = serializedObject.FindProperty ("icon");
		background = serializedObject.FindProperty ("background");
		button = serializedObject.FindProperty ("button");
		
		iconSprite = serializedObject.FindProperty ("iconSprite");
		backgroundColor = serializedObject.FindProperty ("backgroundColor");
		onButtonClick = serializedObject.FindProperty ("onButtonClick");
	}
	
	bool notReady() {
		return !icon.objectReferenceValue || !background.objectReferenceValue || !button.objectReferenceValue;
	}
	
	public override void OnInspectorGUI() {
		serializedObject.Update ();
		
		EditorGUILayout.PropertyField (disableModifiedControls, true);
		
		if (disableModifiedControls.boolValue) {
			serializedObject.ApplyModifiedProperties ();
			return;
		}
		
		if (notReady ()) {
			EditorGUILayout.PropertyField (icon, true);
			EditorGUILayout.PropertyField (background, true);
			EditorGUILayout.PropertyField (button, true);
		} else {
			EditorGUILayout.PropertyField (iconSprite, true);
			EditorGUILayout.PropertyField (backgroundColor, true);
			EditorGUILayout.PropertyField (onButtonClick, true);
		}
		
		serializedObject.ApplyModifiedProperties ();
	}
}

