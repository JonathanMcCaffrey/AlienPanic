using UnityEngine;
using System.Collections;

//TODO Include only what you need. Mobile support?
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.IO;


[Serializable]
[XmlRoot("seg")]
public class LevelSegInfo {
	[XmlAttribute("fileName")]
	public string fileName = "Seg1";
	[XmlAttribute("probablity")]
	public int probablity = 1;
	[XmlAttribute("occursAfter")]
	public int occursAfter = 0;

	public LevelSegInfo() {
	}

	public LevelSegInfo(string fileName, int probablity, int occursAfter) {
		this.fileName = fileName;
		this.probablity = probablity;
		this.occursAfter = occursAfter;
	}

	public LevelInfo Load(string text) {
		return JsonUtility.FromJson<LevelInfo> (text);
	}
}
