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
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public StateGamePlaying stateGamePlaying { get; set; }
	public StateGameWon stateGameWon { get; set; }
	public StateGameLost stateGameLost { get; set; }
	public StateGamePaused stateGamePaused { get; set; }
	public StateGameMenu stateGameMenu { get; set; }
	public StateNewGame stateNewGame { get; set; }
	public StateArtSample stateArtSample { get; set; }
	public StateNotSet stateNotSet { get; set; }

	public static GameManager instance = null;
	private GameState currentState;

	public GameObject inGameMenu;
	public int paused;
    public GameObject audioManagerPrefab;

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

		stateArtSample = GetComponent<StateArtSample>();
		if (!stateArtSample) {
			LogMissingComponent("StateArtSample");
		}

		stateNotSet = GetComponent<StateNotSet>();
		if (!stateNotSet) {
			LogMissingComponent("StateNotSet");
		}
		
		instance = this;

	}

	// Use this for initialization
	void Start () {
		ChangeState (stateNotSet);
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
	
	public void ChangeState (GameState newState) {

//        //TODO remove
//        //Example state change audio 
//        if (AudioManager.Instance && !(newState is StateNotSet))
//        {
//            AudioManager.Instance.PlayClip(AudioManager.Instance.menu.play);
//        }
//
        UpdateAudioByState(newState);

        currentState = newState;
	}

    private void UpdateAudioByState(GameState state)
    {
        if (currentState != state)
        {
            if (state is StateGamePlaying) //TODO: State Manager: StateNewGame is not used in code so changed to StateGamePlaying for now
                AudioManager.Instance.PlayBackground(AudioManager.Instance.gameplay.background[0]);
            else if (state is StateGameWon)
                AudioManager.Instance.PlayBackground(AudioManager.Instance.gameplay.playerWon);
            else if (state is StateGameLost)
                AudioManager.Instance.PlayBackground(AudioManager.Instance.gameplay.playerLost);
            else if (state is StateGamePaused)
                AudioManager.Instance.PlayBackground(AudioManager.Instance.gameplay.gamePause);
            else if (state is StateGameMenu)
                AudioManager.Instance.PlayBackground(AudioManager.Instance.menu.background[0]);
            else
                //TODO Debug: To see why states are being changed soo many times. Remove redundant states later if not required.
                AudioManager.Instance.PlayClip(AudioManager.Instance.menu.play); //Temporary for debugging
        }
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
