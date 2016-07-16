using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SegmentSerializer))]
[CanEditMultipleObjects]
public class SegmentSerializerEditor : Editor {
	SerializedProperty selectedNode;
	SerializedProperty segmentName;
	SerializedProperty enemyType;
	SerializedProperty hotKey;
	
	void OnEnable() {
		selectedNode = serializedObject.FindProperty ("selectedNode");
		segmentName = serializedObject.FindProperty ("segmentName");
		
	}
	
	public override void OnInspectorGUI() {
		serializedObject.Update ();
		
		EditorGUILayout.PropertyField (selectedNode, true);
		EditorGUILayout.PropertyField (segmentName, true);
		
		if (GUILayout.Button ("Save")) {
			if(SegmentSerializer.instance) {
				SegmentSerializer.SaveSelectedNode();
			}
			
		}
		
		if (GUILayout.Button ("Load")) {
			SegmentSerializer.LoadNode();
		}
		
		
		serializedObject.ApplyModifiedProperties ();
	}
}
