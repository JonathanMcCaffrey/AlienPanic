// WinOnCollision.cs
//
// Attach this script to a GameObject with a BoxCollider 2D to change the game state to stateGameWon

using UnityEngine;
using System.Collections;

public class WinOnCollision : MonoBehaviour {

	void  OnCollisionEnter2D (Collision2D other) {
		print ("WinOnCollision::OnCollisionEnter2D() " + other.gameObject.name);
		
		GameManager.instance.ChangeState(GameManager.instance.stateGameWon);
	}
}
