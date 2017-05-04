using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

//TODO Include only what you need. Mobile support?
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.IO;

[Serializable]
[XmlRoot("levelProgress")]
public class LevelProgress {

	[XmlAttribute("highestCompleteLevel")]
	public int highestCompleteLevel = 0;

	[XmlAttribute("lastPlayedLevel")]
	public int lastPlayedLevel = 0;

	public static void currentLevel(LevelInfo info) {
		get ().lastPlayedLevel = info.levelOrder;
	}

	public static void beatedLevel(LevelInfo info) {
		get ().lastPlayedLevel = info.levelOrder;

		if (info.levelOrder > get().highestCompleteLevel) {
			get ().highestCompleteLevel = info.levelOrder;
		}

		StreamWriter reader = new StreamWriter(Application.dataPath + "/Resources/TextLevelData/LevelProgress.txt");
		String json = JsonUtility.ToJson(get());
		reader.Write (json);
		reader.Close ();
	}

	public static int getHighestCompletedLevel() {
		return get ().highestCompleteLevel;
	}

	public static LevelProgress Load(string text) {
		return JsonUtility.FromJson<LevelProgress> (text);
	}

	private static LevelProgress instance = null;
	private static LevelProgress get() {
		if (instance == null) {
			StreamReader reader = new StreamReader(Application.dataPath + "/Resources/TextLevelData/LevelProgress.txt");

			string json = reader.ReadToEnd ();

			instance = LevelProgress.Load (json);
		}

		return instance;
	}
}
