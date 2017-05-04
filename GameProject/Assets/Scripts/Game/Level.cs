using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

	int completionSegs = 7;

	void Start() {
		SegmentManager.spawnedSegCount = 0;
	}

	public void Update() {
		if (SegmentManager.spawnedSegCount > completionSegs) {
			Debug.Log ("Level Completed");

			LevelProgress.beatedLevel (SegmentManager.levelInfo);

			SceneManager.LoadScene ("_LevelSelect");
		}
	}

}
