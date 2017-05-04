/*
 * 	SegmentTriggerVolume.cs
 * 	
 * Contains triggers when a player enters and exists a Segment.
 * 
 * The Trigger Size is the exact size of the Segment, based upon the transforms of inner assets.
 * 
 */

using UnityEngine;
using System.Collections;

public class SegmentTriggerVolume : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			LoadNextLevel ();
		}
	}


	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
		
		}
	}

	void LoadNextLevel () {

		GameObject newSegment = SegmentSerializer.LoadSegmentWithName (SegmentManager.CreateRandomSegmentName ());
		BoxCollider2D levelCollider = gameObject.GetComponent<BoxCollider2D> ();
		float newX = gameObject.transform.position.x + levelCollider.size.x;

		//TODO probably move to placement generation code to the AddNewSegment Function
		newSegment.transform.position = new Vector3 (newX, gameObject.transform.position.y, gameObject.transform.position.z);
		SegmentManager.AddNewSegement (newSegment);

	}
}
