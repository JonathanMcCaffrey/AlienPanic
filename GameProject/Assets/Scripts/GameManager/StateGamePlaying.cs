/*
 *  StateGamePlaying.cs
 *	
 *	
 *
 */

using UnityEngine;
using System.Collections;
using System.Linq;

public class StateGamePlaying : GameState {



	//TODO: Draw the StateGamePlaying GUI here
	public override void StateGUI() {

		// Render the GUI.
		//GUI.Label (new Rect(20, 10, 150, 20), string.Format ("state: GAME PLAYING"));
		if (GameManager.instance.inGameMenu != null) {
			GameManager.instance.inGameMenu.SetActive (false);
		}
	}

	public override void StateUpdate() {

		// print ("StateGamePlaying::StateUpdate() ");

		//TODO: write ProcessGameFlowInput(), to manage the level progression
		//TODO: write UpdateContinuousGameState(), to control object movements, AI, space junk to avoid
		//TODO: update ProcessGameplayInput(), to handle player input
	
		// Handle input from the player here
		// ProcessGameplayInput();

		//Unpause the spacetime
		Time.timeScale = 1;
		GameManager.instance.paused = 0;
	}	
}
