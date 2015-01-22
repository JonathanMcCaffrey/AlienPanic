/*
 * 	GameManager.cs
 * 	
 * 	Currently has 3 states: 
 * 
 * 	stateGamePlaying, stateGameWon, stateGameLost
 * 
 * 	feel free to add new states, 
 * 	and don't even think about touching the currentState variable here, that would be crazy.
 * 
 */

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public StateGamePlaying stateGamePlaying;
	public StateGameWon stateGameWon;
	public StateGameLost stateGameLost;
	public StateGamePaused stateGamePaused;
	public StateGameMenu stateGameMenu;
	public StateNewGame stateNewGame;
	public StateNotSet stateNotSet;

	public static GameManager instance = null;


	private GameState currentState;

	private void Awake () {


		stateGamePlaying = GetComponent<StateGamePlaying>();
		stateGameWon = GetComponent<StateGameWon>();
		stateGameLost = GetComponent<StateGameLost>();
		stateGamePaused = GetComponent<StateGamePaused>();
		stateGameMenu = GetComponent<StateGameMenu>();
		stateNewGame = GetComponent<StateNewGame>();
		stateNotSet = GetComponent<StateNotSet>();
	
		instance = this;
	}



	// Use this for initialization
	void Start () {

		NewGameState (stateNotSet);
	
	}
	
	// Update is called once per frame
	void Update () {

		if (currentState != null)
			currentState.StateUpdate();

	}


	private void OnGUI () {
		if (currentState != null)
			currentState.StateGUI ();
		else print("The current state is NULL");
	}

	public GameState getGameState() {
		return currentState;
	}

	public void NewGameState (GameState newState) {

		 currentState = newState;
	
	}

	public void DisplayCurrentState () {

		if (currentState != null) {
			if (currentState == stateGamePlaying) print("The current state is: stateGamePlaying");
			if (currentState == stateGamePaused) print("The current state is: stateGamePaused");
		}
		else print("The current state is: NULL");

	}

}
