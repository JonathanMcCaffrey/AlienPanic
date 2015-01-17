using UnityEngine;
using System.Collections;

public class StateGameWon : GameState {

	public override void StateGUI() {
		GUILayout.Label ("state: GAME WON");
	}

	//TODO: Save the game, or other stuff before loading the win scene

	public override void StateUpdate() {
		print ("StateGameWon::StateUpdate() ");
		Application.LoadLevel("GameOver"); // TODO: change this to YOU WIN
	}
}
