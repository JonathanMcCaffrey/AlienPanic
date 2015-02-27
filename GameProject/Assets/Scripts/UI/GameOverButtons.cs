using UnityEngine;
using System.Collections;

public class GameOverButtons : MonoBehaviour 
{
	public void Restart ()
	{
		// TODO: Prepare the game for a reset or restart
		GameManager.instance.NewGameState(GameManager.instance.stateGamePlaying);
		Debug.Log ("ResetGame:: Restart -- Loading scene Level_1");
		Application.LoadLevel ("Level_1");
		
	}

	public void MainMenu() {

		GameManager.instance.NewGameState(GameManager.instance.stateGameMenu);
		Application.LoadLevel("MainMenu");
		Debug.Log ("ResetGame:: MainMenu() -- Loading MainMenu");
	}
	
}