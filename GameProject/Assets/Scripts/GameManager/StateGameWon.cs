using UnityEngine;
using System.Collections;

public class StateGameWon : GameState {

	public override void StateGUI() {
		GUILayout.Label ("state: GAME WON");

		GUI.Box(new Rect(10, 500, 200, 100), "GAME STATE MACHINE");
		GUI.Label (new Rect(20, 530, 150, 20), string.Format ("state: GAME PLAYING"));
	}

	//TODO: Save the game, or other stuff before loading the win scene

	public override void StateUpdate() {
		print ("StateGameWon::StateUpdate() -- Saving current level");
		PlayerPrefs.SetString("CurrentLevel", Application.loadedLevelName);
		Application.LoadLevel("Level Complete");
		//TODO: things
	}
}
