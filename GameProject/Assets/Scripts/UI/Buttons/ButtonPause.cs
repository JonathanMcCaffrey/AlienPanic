using UnityEngine;
using System.Collections;

public class ButtonPause : MonoBehaviour {
	public GameObject defaultPauseMenu = null;

	public void onClick() {

		//Pause game logic
		GameManager.instance.NewGameState(GameManager.instance.stateGamePaused);

		// Display the pause menu
		var pauseMenu = GameObject.Instantiate (defaultPauseMenu) as GameObject;
		pauseMenu.transform.parent = gameObject.transform.parent;
		pauseMenu.transform.localScale = Vector3.one;
		pauseMenu.transform.localPosition = Vector3.zero;
	}
}
