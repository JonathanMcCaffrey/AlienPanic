using UnityEngine;
using System.Collections;

public class UIButtons : MonoBehaviour 
{
	public void StartNewGame() {		
		GameManager.instance.NewGameState(GameManager.instance.stateNewGame);
		//TODO: butter scrolls like butter
	}

	public void Restart ()
	{
		//TODO: Get current level
		// restart current level
		//TODO: Use the manager instead of Application.LoadLevel
		GameManager.instance.NewGameState(GameManager.instance.stateGamePlaying);
		Debug.Log ("ResetGame:: Restart -- Loading scene Level_1");
		Application.LoadLevel ("Level 0");
		Application.LoadLevel (PlayerPrefs.GetString("Current Level", "Level 1"));
		
	}
	
	public void MainMenu() {	
		GameManager.instance.NewGameState(GameManager.instance.stateGameMenu);
		Application.LoadLevel("MainMenu");
		Debug.Log ("ResetGame:: MainMenu() -- Loading MainMenu");
	}
	
}