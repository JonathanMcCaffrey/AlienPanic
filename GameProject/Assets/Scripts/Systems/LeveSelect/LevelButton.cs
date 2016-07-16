using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {

	//public string level = "0";

	private LevelInfo levelInfo = new LevelInfo();

	public GameObject defaultLevelButton;

	public void onClick() {
		TextAsset file = Resources.Load<TextAsset>("TextLevelData/Levels/" +  levelInfo.fileName) as TextAsset;

		SegmentManager.levelInfo = levelInfo;


		Application.LoadLevel ("SampleSegment");
	}

	public GameObject Generate(LevelInfo levelInfo) {
		GameObject newButton = Instantiate(Resources.Load<GameObject> ("Prefabs/Menu/LevelButton"));
		LevelButton levelButon = newButton.GetComponent<LevelButton> ();
		levelButon.levelInfo = levelInfo;

		levelButon.GetComponentInChildren<Text> ().text = levelInfo.levelName;

		return newButton;
	}
}
