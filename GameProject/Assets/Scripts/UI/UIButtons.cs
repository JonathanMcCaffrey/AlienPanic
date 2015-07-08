//
// Buttons!
//

using UnityEngine;
using System.Collections;

public class UIButtons : MonoBehaviour 
{
	public void StartNewGame() {		
		GameManager.instance.ChangeState(GameManager.instance.stateNewGame);
		//TODO: butter scrolls like butter
	}

	public void StartArtSample() {		
		GameManager.instance.ChangeState(GameManager.instance.stateArtSample);
		//TODO: butter scrolls like butter
	}

	public void Restart ()
	{
		//TODO: Get current level
		// restart current level
		//TODO: Use the manager instead of Application.LoadLevel
		GameManager.instance.ChangeState(GameManager.instance.stateGamePlaying);
		Debug.Log ("ResetGame:: Restart -- Loading scene Level_1");
		Application.LoadLevel ("Level 0");
		// Application.LoadLevel (PlayerPrefs.GetString("CurrentLevel", "Level 0"));
		
	}
	
	public void MainMenu() {	
		GameManager.instance.ChangeState(GameManager.instance.stateGameMenu);
		Debug.Log ("ResetGame:: MainMenu() -- Loading MainMenu");
		Application.LoadLevel("MainMenu");
	}

	public void NextLevel() {
		//TODO: fix this super hacky
		Application.LoadLevel(Application.loadedLevel+1);
		// PlayerPrefs.GetString("CurrentLevel");
	}
	
}