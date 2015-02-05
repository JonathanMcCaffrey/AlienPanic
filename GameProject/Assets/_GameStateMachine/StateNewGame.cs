using UnityEngine;
using System.Collections;

public class StateNewGame : GameState {
	
	public override void StateGUI() {
		GUILayout.Label ("state: NEW GAME");
	}
	
	// Starting a new game, for now loads Level_1
	
	public override void StateUpdate() {
		print ("StateNewGame::StateUpdate() ");

		Application.LoadLevel("Level_1");
	}
}
