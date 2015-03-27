using UnityEngine;
using System.Collections;

public class StateNotSet : GameState {
	
	public override void StateGUI() {
//		GUILayout.Label ("state: Not Set");
//
//		bool pauseGameButtonClicked = GUILayout.Button ("PAUSE");
//		if (pauseGameButtonClicked) gameManager.NewGameState(gameManager.stateGamePaused);
//
//		bool playGameButtonClicked = GUILayout.Button ("PLAY");
//		if (playGameButtonClicked) gameManager.NewGameState(gameManager.stateGamePlaying);
	}
	
	// Starting a new game, for now loads Level_1
	
	public override void StateUpdate() {
		// print ("StateUnset::StateUpdate() -- The Game State was initialized but not set!");

		// Change the game state to stateGamePlaying if the Character object is in the scene
		if (GameObject.Find("Character")) {
			print ("StateNotSet::StateUpdate() -- Character found, changing state to stateGamePlaying");
			gameManager.ChangeState(gameManager.stateGamePlaying);
		}
	}
}
