using UnityEngine;
using System.Collections;

public class StateLevelSelect : GameState {
	
	public override void StateGUI() {
		GUILayout.Label ("state: LEVEL Select");
	}

	public override void StateUpdate() {
		print ("StateLevelSelect::StateUpdate() -- Starting level select");
	
		Application.LoadLevel("_LevelSelect");
	}
}
