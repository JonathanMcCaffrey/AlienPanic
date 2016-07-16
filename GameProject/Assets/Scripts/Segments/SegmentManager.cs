/*
 * 	SegmentManager.cs
 * 	
 * 	Levels are currently broken into small segments for load forever: 
 * 
 * 	MaxSegmentCount currently controls how many Segments can exist in list.
 * 
 * 	
 * 
 */


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SegmentManager {

	public static LevelInfo levelInfo;

	List<GameObject> list = null;

	const int MaxSegmentCount = 3;


	private SegmentManager() {
		list = new List<GameObject> ();

	}

	private static SegmentManager instance = null;
	private static SegmentManager get() {
		if (instance == null) {
			instance = new SegmentManager();
		}

		return instance;
	}

	public static void AddNewSegement(GameObject newSegment) {
		if (get ().list.Count >= MaxSegmentCount) {
			GameObject oldSegment = get ().list[0];
			get ().list.RemoveAt(0);
			GameObject.Destroy(oldSegment);
		}

		get ().list.Add (newSegment);
	}

	public static int spawnedSegCount = 0;
	public static string CreateRandomSegmentName() {
		if (levelInfo == null) {
			return "Seg1";
		}

		List<LevelSegInfo> segChances = new List<LevelSegInfo>();

		for (int segIndex = 0; segIndex < levelInfo.levelSegList.Count; segIndex++) {
			LevelSegInfo segInfo = levelInfo.levelSegList [segIndex];

			if (segInfo.occursAfter <= spawnedSegCount) {
				for (int chanceIndex = 0; chanceIndex < segInfo.probablity; chanceIndex++) {
					segChances.Add (segInfo);
				}
			}
		}
			
		int chosenSeg = Random.Range (0, segChances.Count);

		spawnedSegCount++;

		return segChances [chosenSeg].fileName;
	}
}
