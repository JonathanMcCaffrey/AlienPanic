using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	
	// Score
	private int currentScore;
	
	// High Score
	private int highScore;

	// The key for saving HighScore with PlayerPrefs
	private string currentScoreKey = "currentScore";

	// The key for saving HighScore with PlayerPrefs
	private string highScoreKey = "highScore";
	
	// Use this for initialization
	void Start () {
		Initialize ();
	}
	
	// Update is called once per frame
	void Update () {
		// If the Score is higher than the High Score
		if (highScore < currentScore) {
			highScore = currentScore;
		}
	}
	
	// Return to the original game state
	private void Initialize () {
		
		// Retrieve the current score.  If it can't be received, use zero
		highScore = PlayerPrefs.GetInt (currentScoreKey, 0);
		
		// Retrieve the high score.  If it can't be received, use zero
		highScore = PlayerPrefs.GetInt (highScoreKey, 0);
	}
	
	// Adding points
	public void AddPoint (int point) {
		currentScore = currentScore + point;
	}
	
	// Saving the High Score
	public void Save (){

		// Save the current score
		PlayerPrefs.SetInt (currentScoreKey, highScore);
		PlayerPrefs.Save ();
		
		// Save the high score
		PlayerPrefs.SetInt (highScoreKey, highScore);
		PlayerPrefs.Save ();
		
		// Return to the original game state
		Initialize ();
	}
	
	void OnGUI() {
		// Render the GUI.
		GUI.Box(new Rect(350, 10, 100, 40), "High Score");
		GUI.Label (new Rect(395, 30, 150, 20), string.Format ("{0:0}", highScore));
		
		GUI.Box(new Rect(600, 10, 100, 40), "Score");
		GUI.Label (new Rect(645, 30, 20, 20), string.Format ("{0:0}", currentScore));
		
	}
}
