using UnityEngine;
using System.Collections;

public class Geyser : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col) {
		Health health = col.collider.GetComponent<Health>();
		if (health) {
			health.ChangeHealth(-1);
		}
	}
}
