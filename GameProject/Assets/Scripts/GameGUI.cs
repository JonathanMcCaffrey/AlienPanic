using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {
	GameObject player;
	float playerStartXCoord;	// Player's starting x-coordinate.
	float playerDistance;		// Distance travelled since the start of the game.
	Vector2 playerVelocity;		// Player's current velocity.

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		if (!player) {
			Debug.LogError ("Unable to initialize GameGUI: No game object has the Player tag.");
		}

		playerStartXCoord = player.transform.position.x;
		playerDistance = 0;
		playerVelocity = new Vector2();
	}

	/// <summary>
	/// Renders the GUI.
	/// </summary>
	void OnGUI() {
		// Update the player stats.
		if (player) {
			playerDistance = player.transform.position.x - playerStartXCoord;
			playerVelocity = new Vector2(player.rigidbody2D.velocity.x, player.rigidbody2D.velocity.y);
		}

		// Render the GUI.
		GUI.Box(new Rect(10, 10, 200, 70), "Stats");
		GUI.Label (new Rect(20, 30, 150, 20), string.Format ("Distance: {0:0}", playerDistance));
		GUI.Label (new Rect(20, 50, 150, 20), string.Format ("Velocity: ({0:0.0}, {1:0.0})", playerVelocity.x, playerVelocity.y));
	}
}
