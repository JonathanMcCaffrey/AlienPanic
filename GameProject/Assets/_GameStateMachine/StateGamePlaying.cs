/*
 *  StateGamePlaying.cs
 *	
 *	
 *
 */

using UnityEngine;
using System.Collections;

public class StateGamePlaying : GameState {

	bool movingUp = false;		// Flag to indicate if the player is in a moving-up state.
	float particleTimer = 0;	// Timer used to delay when the particles start / stop emitting.
	
	//TODO: Draw the StateGamePlaying GUI here
	public override void StateGUI() {

		GUILayout.Label("state: GAME PLAYING");

	}

	public override void StateUpdate() {

		// print ("StateGamePlaying::StateUpdate() ");

		//TODO: write ProcessGameFlowInput() //Reset or next level
		//TODO: write UpdateContinuousGameState() //moving objects here, AI, space junk to avoid
		//TODO: update ProcessGameplayInput(), detatch particle stuff from player movement (maybe --a)
	
		// Handle input from the player here
		// ProcessGameplayInput();
	}

	//TODO: Trigger events? Maybe later...
	void  OnTriggerEnter2D (Collider2D other) {
		print ("StateGamePlaying::OnTriggerEnter2D() ");
		//Destroy(other.gameObject);
	
	}

	//TODO: Collide with other stuff here
	void  OnCollisionEnter2D (Collision2D other) {
		print ("StateGamePlaying::OnCollisionEnter2D() ");

		if (other.gameObject.tag == "LevelWin")
			gameManager.NewGameState (gameManager.stateGameWon);
	}

	//I combined this is from DemoCharacterController.cs , might need a fix. Looks ok to me --andre
	void ProcessGameplayInput() {
	}
}
