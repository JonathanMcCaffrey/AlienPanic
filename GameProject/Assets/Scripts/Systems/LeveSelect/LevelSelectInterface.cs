using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectInterface : MonoBehaviour {


	public List<LevelButton> levels; 

	// Use this for initialization
	void Start () {
		List<LevelInfo> levelInfo = LevelSelect.getLevels ();

		int columnLength = 3;
		int current = 0;
		int row = 0;

		float height = 120;
		float width = 150;


		levelInfo.Sort(delegate(LevelInfo a, LevelInfo b) {
			return (a.levelOrder).CompareTo(b.levelOrder);
		});

		for (int levelIndex = 0; levelIndex < levelInfo.Count; levelIndex++) {
			if (current >= columnLength) {
				row++;
				current = 0;
			}

			GameObject levelButton = new LevelButton().Generate (levelInfo [levelIndex]);
			levelButton.transform.parent = gameObject.transform;

			levelButton.transform.position = new Vector3 (current * width + 90, -height * row + 350, 0);

			current++;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
