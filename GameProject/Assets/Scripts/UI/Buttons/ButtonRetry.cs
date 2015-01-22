using UnityEngine;
using System.Collections;

public class ButtonRetry : MonoBehaviour {

	public void onClick() {
		Application.LoadLevel ("Level_1");
		//TODO Reset Logic

		//Reset score to zero

		Score.instance.ResetScore ();
		GameManager.instance.NewGameState(GameManager.instance.stateGamePlaying);
	}
}
