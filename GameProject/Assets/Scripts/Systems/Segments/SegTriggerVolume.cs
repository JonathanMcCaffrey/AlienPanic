using UnityEngine;
using System.Collections;

public class SegTriggerVolume : MonoBehaviour {


	//TODO Cleaner
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {

		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			GameObject rootLevel = SceneSaver.LoadNodeAtPath("Seg1");

			BoxCollider2D levelCollider = gameObject.GetComponent<BoxCollider2D>();

			float newX = gameObject.transform.position.x + levelCollider.size.x;

			rootLevel.transform.position = new Vector3(newX,gameObject.transform.position.y,gameObject.transform.position.z);

			Destroy(gameObject);	
		}
	}
}
