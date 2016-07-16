using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//TODO Include only what you need. Mobile support?
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.IO;

[Serializable]
[XmlRoot("level")]
public class LevelInfo {
	[XmlAttribute("levelOrder")]
	public int levelOrder = 0;

	[XmlAttribute("levelName")]
	public string levelName = "";

	[XmlAttribute("fileName")]
	public string fileName = "";

	[XmlAttribute("levelSeg")] //TODO look into formatting
	public List<LevelSegInfo> levelSegList = new List<LevelSegInfo>();

	public static LevelInfo Load(string text) {
		return JsonUtility.FromJson<LevelInfo> (text);
	}

}
