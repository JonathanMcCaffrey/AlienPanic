using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TabPlacementData {
	public string filePath;
	public string FilePath {
		get { return filePath; }
	}
	
	public string name;
	public string Name {
		get { return name; }
	}
	
	public TabPlacementData(string filePath = "", string name = "") {
		this.filePath = filePath;
		this.name = name;
	}
}

