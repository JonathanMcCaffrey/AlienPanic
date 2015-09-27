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
		newSegment.transform.position = new Vector3 (newX, gameObject.transform.position.y, gameObject.transform.position.z);
		SegmentManager.AddNewSegement (newSegment);

	}
}
