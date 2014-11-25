using UnityEngine;
using System.Collections;

public class GeyserEruption : MonoBehaviour {
	public Vector2 force;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.rigidbody2D) {
			col.rigidbody2D.AddForce(force);
		}
	}
}
