using UnityEngine;
using System.Collections;

public class StateGameLost : GameState {
	
	public override void StateGUI() {
		GUILayout.Label ("state: GAME LOST");
	}

	//TODO: Prepare the game for the GAME OVER scene and load the GAME OVER scene
	public override void StateUpdate() {
		print ("StateGameLost::StateUpdate() ");
		Application.LoadLevel("Game Over");
	}
}
