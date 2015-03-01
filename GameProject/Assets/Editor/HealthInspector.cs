using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Health), true)]
public class HealthInspector : Editor {

	public override void OnInspectorGUI() {
		Health healthScript = (Health)target;

		DrawDefaultInspector();

		EditorGUILayout.LabelField ("Health", string.Format ("{0}/{1}", healthScript.HealthRemaining, healthScript.maxHealth));
		EditorGUILayout.LabelField ("Is Alive", (healthScript.IsAlive() ? "true" : "false"));
	}
}
