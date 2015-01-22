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

	private GameState currentState;

	private void Awake () {

		stateGamePlaying = GetComponent<StateGamePlaying>();
		stateGameWon = GetComponent<StateGameWon>();
		stateGameLost = GetComponent<StateGameLost>();
	
	}

	// Use this for initialization
	void Start () {

		NewGameState (stateGamePlaying);
	
	}
	
	// Update is called once per frame
	void Update () {

		if (currentState != null)
			currentState.StateUpdate();

	}


	private void OnGUI () {
		if (currentState != null)
			currentState.StateGUI ();
	
	}


	public void NewGameState (GameState newState) {

		currentState = newState;
	
	}
}
