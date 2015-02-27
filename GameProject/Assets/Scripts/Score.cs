using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	public static Score instance = null;
	
	// Score
	private int currentScore;
	
	// High Score
	private int highScore;

	// The key for saving HighScore with PlayerPrefs
	private string currentScoreKey = "currentScore";

	// The key for saving HighScore with PlayerPrefs
	private string highScoreKey = "highScore";

	void Awake () {
		if (instance) {
			//Destroy (gameObject);
		} else {
			
			/*Debug.Log("Additional Player Deleted");

			DontDestroyOnLoad(gameObject);*/
			
			instance = this;
		}
	}
	
	// Use this for initialization
	void Start () {
		Initialize ();
	}
	
	// Update is called once per frame
	void Update () {
		// If the Score is higher than the High Score
		if (highScore < currentScore) {
			highScore = currentScore;
			SaveHighScore ();
		}
	}
	
	// Return to the original game state
	private void Initialize () {
		
		// Retrieve the current score.  If it can't be received, use zero
		highScore = PlayerPrefs.GetInt (currentScoreKey, 0);
		
		// Retrieve the high score.  If it can't be received, use zero
		highScore = PlayerPrefs.GetInt (highScoreKey, 0);
		
		Debug.Log ("Score:: Initialize() -- Loaded highscore: " + highScore);
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

	public void SaveHighScore () {

		Debug.Log ("Score:: SaveHighScore() -- Saving highscore: " + highScore);

		PlayerPrefs.SetInt (highScoreKey, highScore);
		PlayerPrefs.Save ();
	}

	//Clear the score (to be used during a game reset or end)
	public void ResetScore () {
		currentScore = 0;
		SaveHighScore ();
		Initialize ();
	}
}
