using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LevelSelect {

	List<LevelInfo> levelList = new List<LevelInfo>();

	static public List<LevelInfo> getLevels() { 
		return get ().levelList;
	}

	public GameObject levelButtonDefault;

	private static LevelSelect instance = null;
	private static LevelSelect get() {
		if (instance == null) {
			instance = new LevelSelect();
			instance.Initialize ();
		}

		return instance;
	}

	void Initialize () {
		string [] files = Directory.GetFiles (Application.dataPath + "/Resources/TextLevelData/Levels");

		for (int levelIndex = 0; levelIndex < files.Length; levelIndex++) {
			if (files [levelIndex].Contains (".DSStore") || files [levelIndex].Contains (".DS_Store") || files [levelIndex].Contains (".meta")) {

			} else {
				StreamReader reader = new StreamReader(files [levelIndex]);

				string json = reader.ReadToEnd ();

				levelList.Add (LevelInfo.Load (json));


			}
		}
	}

	void Generate() {
		/*
		LevelInfo levelInfo = new LevelInfo ();
		levelInfo.levelOrder = 3;
		levelInfo.levelName = "Example";
		levelInfo.fileName = "Level1";

		LevelSegInfo seg1 = new LevelSegInfo ();
		seg1.fileName = "1";
		seg1.occursAfter = 0;
		seg1.probablity = 1;

		LevelSegInfo seg2 = new LevelSegInfo ();
		seg2.fileName = "2";
		seg2.occursAfter = 2;
		seg2.probablity = 3;

		LevelSegInfo seg3 = new LevelSegInfo ();
		seg3.fileName = "3";
		seg3.occursAfter = 4;
		seg3.probablity = 3;

		LevelSegInfo seg4 = new LevelSegInfo ();
		seg4.fileName = "4";
		seg4.occursAfter = 6;
		seg4.probablity = 6;

		levelInfo.levelSegList.Add (seg1);
		levelInfo.levelSegList.Add (seg2);
		levelInfo.levelSegList.Add (seg3);
		levelInfo.levelSegList.Add (seg4);

		string json = JsonUtility.ToJson(levelInfo);

		StreamWriter sr = File.CreateText("/Users/jonathanmccaffrey/Desktop/AlienPanic-Repo/GameProject/Assets/Resources/TextLevelData/Levels/test2.txt");
		sr.WriteLine (json);
		sr.Close();
		*/	
	}
}
