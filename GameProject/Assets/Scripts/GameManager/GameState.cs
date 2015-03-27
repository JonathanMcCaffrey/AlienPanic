/*
	GameState.cs
*/
using UnityEngine;
using System.Collections;

public abstract class GameState : MonoBehaviour {

	protected GameManager gameManager;

	protected void Awake() {
		gameManager = GetComponent<GameManager>();
	}
	public abstract void StateUpdate();
	public abstract void StateGUI();
}
