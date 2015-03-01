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

	public StateGamePlaying stateGamePlaying { get; set; }
	public StateGameWon stateGameWon { get; set; }
	public StateGameLost stateGameLost { get; set; }
	public StateGamePaused stateGamePaused { get; set; }
	public StateGameMenu stateGameMenu { get; set; }
	public StateNewGame stateNewGame { get; set; }
	public StateNotSet stateNotSet { get; set; }

	public static GameManager instance = null;
	private GameState currentState;

	private void Awake () {
		stateGamePlaying = GetComponent<StateGamePlaying>();
		if (!stateGamePlaying) {
			LogMissingComponent("StateGamePlaying");
		}

		stateGameWon = GetComponent<StateGameWon>();
		if (!stateGameWon) {
			LogMissingComponent("StateGameWon");
		}

		stateGameLost = GetComponent<StateGameLost>();
		if (!stateGameLost) {
			LogMissingComponent("StateGameLost");
		}

		stateGamePaused = GetComponent<StateGamePaused>();
		if (!stateGamePaused) {
			LogMissingComponent("StateGamePaused");
		}

		stateGameMenu = GetComponent<StateGameMenu>();
		if (!stateGameMenu) {
			LogMissingComponent("StateGameMenu");
		}

		stateNewGame = GetComponent<StateNewGame>();
		if (!stateNewGame) {
			LogMissingComponent("StateNewGame");
		}

		stateNotSet = GetComponent<StateNotSet>();
		if (!stateNotSet) {
			LogMissingComponent("StateNotSet");
		}
		
		instance = this;
	}

	// Use this for initialization
	void Start () {
		NewGameState (stateNotSet);
	}
	
	// Update is called once per frame
	void Update () {
		if (currentState != null) {
			currentState.StateUpdate();
		}
	}

	private void OnGUI () {
		if (currentState != null) {
			currentState.StateGUI ();
		} else {
			print("The current state is NULL");
		}
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
		else {
			print("The current state is: NULL");
		}
	}

	void LogMissingComponent(string componentName) {
		Debug.LogError("Unable to start GameManager: Component " + componentName + " isn't set.");
	}
}
