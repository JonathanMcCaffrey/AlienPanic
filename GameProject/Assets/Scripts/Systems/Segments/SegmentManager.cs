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

	//TODO Make this better
	public static int segCount = 0;

	//Some players demoing at booth wanted some lose/win state.
	public static string CreateRandomSegmentName() {

		if (segCount > 12) {
			segCount = 0;
			Debug.Log ("ResetGame:: MainMenu() -- Loading MainMenu");
			Application.LoadLevel("MainMenu");
		}

		if (segCount > 7) {
			bool isLose = Random.Range (0, 2) == 0;
			if(isLose) {
				
				return "LoseSeg";

			} else {
				return "MissleSeg";
			}

		}

		bool isMissle = Random.Range (0, 2) == 0;

		if(segCount > 1 && isMissle) {
			return "MissleSeg";
		}

		segCount++;

		return "Seg1";
	}

}
