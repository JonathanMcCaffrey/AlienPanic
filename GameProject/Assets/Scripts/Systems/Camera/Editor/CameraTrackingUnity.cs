using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CameraTracking))]
[CanEditMultipleObjects]
public class CameraTrackingUnity : Editor {
	
	SerializedProperty cameraOffsetY;
	SerializedProperty defaultQuadrant;
	SerializedProperty quadrantContainer;
	SerializedProperty distanceThreshold;
	SerializedProperty startY;
	
	SerializedProperty quadrants;
	
	SerializedProperty thresholdDisplay;

	SerializedProperty isVisible;
	SerializedProperty isEditorGuide;

	
	void OnEnable() {
		cameraOffsetY = serializedObject.FindProperty ("cameraOffsetY");
		defaultQuadrant = serializedObject.FindProperty ("defaultQuadrant");
		quadrantContainer = serializedObject.FindProperty ("quadrantContainer");
		distanceThreshold = serializedObject.FindProperty ("distanceThreshold");
		startY = serializedObject.FindProperty ("startY");
		
		quadrants = serializedObject.FindProperty ("quadrants");
		
		thresholdDisplay = serializedObject.FindProperty ("thresholdDisplay");

		isVisible = serializedObject.FindProperty ("isVisible");
		isEditorGuide = serializedObject.FindProperty ("isEditorGuide");
	}
	
	bool notReady() {
		return !defaultQuadrant.objectReferenceValue || !quadrantContainer.objectReferenceValue;
	}
	
	void CreateQuadrantUI () {
		GUILayout.Label ("Quadrants: " + quadrants.arraySize);
		var rect = EditorGUILayout.BeginHorizontal ("Button");
		rect.width = rect.width / 2;
		var style = new GUIStyle ();
		style.alignment = TextAnchor.MiddleCenter;
		if (quadrants.arraySize < 8) {
			if (GUI.Button (rect, GUIContent.none)) {
				quadrants.InsertArrayElementAtIndex (0);
				var container = quadrantContainer.objectReferenceValue as GameObject;
				var quad = GameObject.Instantiate (defaultQuadrant.objectReferenceValue) as GameObject;
				quad.name = "CameraQudrant_" + quadrants.arraySize;
				quad.transform.parent = container.transform;
				serializedObject.ApplyModifiedProperties ();
			}
		}
		GUILayout.Label ("Add", style);
		if (quadrants.arraySize > 0) {
			rect.x = rect.x + rect.width;
			if (GUI.Button (rect, GUIContent.none)) {
				DestroyImmediate (quadrants.GetArrayElementAtIndex (quadrants.arraySize - 1).objectReferenceValue);
				serializedObject.ApplyModifiedProperties ();
			}
		}
		GUILayout.Label ("Remove", style);
		
		EditorGUILayout.EndHorizontal ();
	}
	
	public override void OnInspectorGUI() {
		serializedObject.Update ();
		
		if (!thresholdDisplay.objectReferenceValue) {
			EditorGUILayout.PropertyField (thresholdDisplay, true);
		}
		
		if (notReady()) {
			EditorGUILayout.PropertyField (defaultQuadrant, true);
			EditorGUILayout.PropertyField (quadrantContainer, true);
		} else {
			EditorGUILayout.PropertyField (cameraOffsetY, true);
			EditorGUILayout.PropertyField (distanceThreshold, new GUIContent("Threshold"), true, null);
			EditorGUILayout.PropertyField (startY, true);

			CreateQuadrantUI ();

			EditorGUILayout.PropertyField (isVisible, true);
			EditorGUILayout.PropertyField (isEditorGuide, true);
		}
		
		serializedObject.ApplyModifiedProperties ();
	}
}