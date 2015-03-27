using UnityEngine;
using System.Collections;

public class StateGameLost : GameState {
	
	public override void StateGUI() {
		GUILayout.Label ("state: GAME LOST");
	}

	//TODO: Prepare the game for the GAME OVER and load the GAME OVER scene
	public override void StateUpdate() {

		print ("StateGameLost::StateUpdate() ");
		GameManager.instance.ChangeState(GameManager.instance.stateGameLost);

		Application.LoadLevel("GameOver");

	}
}
