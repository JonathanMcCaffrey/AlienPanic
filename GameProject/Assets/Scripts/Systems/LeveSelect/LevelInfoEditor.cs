using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LevelInfoEditor : MonoBehaviour {

	public List<LevelInfo> list = new List<LevelInfo>();

	[ContextMenu("Load")] 
	public void load() {
		list.Clear ();

		string [] files = Directory.GetFiles (Application.dataPath + "/Resources/TextLevelData/Levels");

		for (int levelIndex = 0; levelIndex < files.Length; levelIndex++) {
			if (files [levelIndex].Contains (".DSStore") || files [levelIndex].Contains (".DS_Store") || files [levelIndex].Contains (".meta")) {

			} else {
				StreamReader reader = new StreamReader(files [levelIndex]);
				string json = reader.ReadToEnd ();
				list.Add (new LevelInfo().Load (json));
			}
		}
	}

	[ContextMenu("Save")] 
	public void save() {

		for (int levelIndex = 0; levelIndex < list.Count; levelIndex++) {
			string file = Application.dataPath + "/Resources/TextLevelData/Levels/" + list [levelIndex].fileName + ".txt";
			StreamWriter writer = new StreamWriter(file);
			string json = JsonUtility.ToJson (list [levelIndex]);
			writer.Write (json);
			writer.Close ();
		}
	}
}
