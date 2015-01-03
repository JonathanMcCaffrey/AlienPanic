using UnityEngine;
using UnityEditor;
 
// While this window is open editor transform movement/rotation will auto snap to grid
public class AutoGridSnapWindow : EditorWindow
{
	[MenuItem("Edit/Commands/Toggle Auto Grid Snapping %_l")]
    static void ToggleGridSnap()
    {
        bool doSnap = EditorPrefs.GetBool("AutoGridSnap.doSnap");
        doSnap = !doSnap;
        EditorPrefs.SetBool("AutoGridSnap.doSnap", doSnap);

        AutoGridSnapWindow window = (AutoGridSnapWindow)EditorWindow.GetWindow(typeof(AutoGridSnapWindow));
        window.Repaint();
    }

    [MenuItem( "Edit/Auto Snap" )]
    static void Init()
    {
		// Create window
	    AutoGridSnapWindow window = (AutoGridSnapWindow)EditorWindow.GetWindow( typeof( AutoGridSnapWindow ) );
	    window.maxSize = new Vector2( 300, 200 );
    }
     
    public void OnGUI()
    {
        // Allow for on the fly value changes
	    EditorPrefs.SetBool("AutoGridSnap.doSnap", EditorGUILayout.Toggle( "Auto Snap", EditorPrefs.GetBool("AutoGridSnap.doSnap", false)));
	    EditorPrefs.SetFloat("AutoGridSnap.MoveSnapX", EditorGUILayout.FloatField( "X Snap Value", EditorPrefs.GetFloat("AutoGridSnap.MoveSnapX", 1.0f)));
		EditorPrefs.SetFloat("AutoGridSnap.MoveSnapY", EditorGUILayout.FloatField( "Y Snap Value", EditorPrefs.GetFloat("AutoGridSnap.MoveSnapY", 1.0f)));
		EditorPrefs.SetFloat("AutoGridSnap.MoveSnapZ", EditorGUILayout.FloatField( "Z Snap Value", EditorPrefs.GetFloat("AutoGridSnap.MoveSnapZ", 1.0f)));
		EditorPrefs.SetFloat("AutoGridSnap.RotationSnap", EditorGUILayout.FloatField( "Rotate Snap", EditorPrefs.GetFloat("AutoGridSnap.RotationSnap", 45.0f)));
	}
}