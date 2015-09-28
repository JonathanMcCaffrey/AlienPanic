using UnityEngine;
using System.Collections;

public class Magnet : MonoBehaviour {

	public float maximumForce = 50.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			Vector2 toPlayer = (transform.position - col.transform.position);
			float forceMagnitude = 1.0f - (toPlayer.magnitude/((CircleCollider2D)GetComponent<Collider2D>()).radius); 
			//forceMagnitude *= forceMagnitude; //Use squared falloff
			forceMagnitude *= maximumForce;
			Vector2 force = toPlayer.normalized * forceMagnitude;
			col.GetComponent<Rigidbody2D>().AddForce(force);
		}
	}
}
