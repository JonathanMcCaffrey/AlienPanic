//
// Buttons!
//

using UnityEngine;
using System.Collections;

public class UIButtons : MonoBehaviour 
{
	public void StartNewGame() {		
		GameManager.instance.ChangeState(GameManager.instance.stateNewGame);
	}

	public void StartArtSample() {		
		GameManager.instance.ChangeState(GameManager.instance.stateArtSample);
	}

	public void Restart ()
	{
		//TODO: Get current level
		// restart current level
		//TODO: Use the manager instead of Application.LoadLevel

		SegmentManager.spawnedSegCount = 0;

		GameManager.instance.ChangeState(GameManager.instance.stateGamePlaying);
		Debug.Log ("ResetGame:: Restart -- Loading scene Level_1");
		Application.LoadLevel ("Level 0");
		// Application.LoadLevel (PlayerPrefs.GetString("CurrentLevel", "Level 0"));
		
	}
	
	public void MainMenu() {	
		GameManager.instance.ChangeState(GameManager.instance.stateGameMenu);
		Debug.Log ("ResetGame:: MainMenu() -- Loading MainMenu");
		Application.LoadLevel("_MainMenu");
	}

	public void NextLevel() {
		//TODO: fix this super hacky
		Application.LoadLevel(Application.loadedLevel+1);
		// PlayerPrefs.GetString("CurrentLevel");
	}

	public void TogglePause() {
		//TODO: fix this super hacky
		if(GameManager.instance.paused == 0)
			GameManager.instance.ChangeState(GameManager.instance.stateGamePaused);
		if(GameManager.instance.paused == 1)
			GameManager.instance.ChangeState(GameManager.instance.stateGamePlaying);
		Debug.Log ("Pause");
	}

	public void Pause() {
		//TODO: fix this super hacky
		GameManager.instance.ChangeState(GameManager.instance.stateGamePaused);
		Debug.Log ("Pause");
	}

	public void Resume() {
		//TODO: fix this super hacky
		GameManager.instance.ChangeState(GameManager.instance.stateGamePlaying);
		Debug.Log ("Resume");
	}
}