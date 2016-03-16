using UnityEngine;
using System.Collections;

public class DroidMotion : MonoBehaviour {

	public float speed = 1.0f; // speed of motion of the droid
	public float headstart = 20.0f; // distance before droid starts chasing you

	Animator anim;
	GameObject player;
	Vector3 movement;
	bool chasing = false;

	// Use this for initialization
	void Awake () {
		anim = GetComponent <Animator> ();
		anim.SetBool ("DetectedPlayer", false);

		player = GameObject.FindWithTag("Player");	
	}
	
	// Update is called once per frame
	void Update () {
		float delta_x, delta_y;
		if (chasing == true) {
			delta_x = player.transform.position.x - transform.position.x;
			delta_y = player.transform.position.y - transform.position.y;		


			movement.Set (-delta_x, delta_y, 0f);
			movement = movement.normalized * speed * Time.deltaTime;
			transform.Translate(movement);
			//playerRigidbody.MovePosition (transform.position + movement);
		}
		else if (player.transform.position.x > (transform.position.x + headstart)) {
			anim.SetBool ("DetectedPlayer", true);
			Debug.Log ("Detected");
			chasing = true;
		} else {
			Debug.Log ("no seen");
		}

	
	}
}
