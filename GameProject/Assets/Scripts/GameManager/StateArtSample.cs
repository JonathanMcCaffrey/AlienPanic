using UnityEngine;
using System.Collections;

public class StateArtSample : GameState {
	
	public override void StateGUI() {
		GUILayout.Label ("state: ART SAMPLE");
	}

	public override void StateUpdate() {
		print ("StateArtSample::StateUpdate() -- Starting art sample");
	
		Application.LoadLevel("Level 1");
	}
}
