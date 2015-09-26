using UnityEngine;
using System.Collections;

public class StateNewGame : GameState {
	
	public override void StateGUI() {
		GUILayout.Label ("state: NEW GAME");
	}
	
	// Starting a new game, for now loads Level_0
	
	public override void StateUpdate() {
		print ("StateNewGame::StateUpdate() -- Starting a new game");
		//TODO: reset the current score and other non high-score playerdata

		Application.LoadLevel("SampleSegment");
	}
}
