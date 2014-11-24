using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {
	GameObject player;
	float playerStartXCoord;	// Player's starting x-coordinate.
	float playerDistance;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		if (!player) {
			Debug.LogError ("Unable to initialize GameGUI: No game object has the Player tag.");
		}

		playerStartXCoord = player.transform.position.x;
		playerDistance = 0;
	}

	/// <summary>
	/// Renders the GUI.
	/// </summary>
	void OnGUI() {
		// Make a background box.
		GUI.Box(new Rect(10, 10, 200, 50), "Stats");

		// Print the distance travelled.
		if (player) {
			playerDistance = player.transform.position.x - playerStartXCoord;
		}
		GUI.Label (new Rect(20, 30, 150, 20), string.Format ("Distance: {0:0}", playerDistance));
	}
}
