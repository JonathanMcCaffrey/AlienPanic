using UnityEngine;
using System.Collections;
using System;

public class DroidMotion : MonoBehaviour {

	public float speed = 1.0f; // speed of motion of the droid
	public float headstart = 20.0f; // distance before droid starts chasing you
	public float contact = 0.5f; // The distance to the player to make contact
	public float moveSize = 0.2f; // For the up/down movement when Idling
	public float rotSpeed = 0.1f;  // Speed at which to rotate when droid is finished
	public float O2_Drain = -30.0f;

	Animator anim;
	GameObject player;
	PlayerHealth HealthScript;
	Vector3 movement;
	bool chasing = false;
	bool finished = false; // set to true after the droid has hurt the player
	float time = 0;
	float startPosition = -1;

	// Use this for initialization
	void Awake () {
		anim = GetComponent <Animator> ();
		anim.SetBool ("DetectedPlayer", false);

		player = GameObject.FindWithTag("Player");	
		HealthScript = player.GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		float delta_x, delta_y;

		if (player == null) // exit the update function if the player is dead
			return;

		if(finished == true){ // IN THIS STATE THE DROID HAS ALREADY DAMAGED THE PLAYER AND IS DONE
			anim.SetBool ("DetectedPlayer", false); // set to false to return in the default animation state
			move ();  // move up and downn
			transform.Rotate (new Vector3 (0.0f, rotSpeed, 0.0f));
		}
		else if (chasing == true) { // IN THIS STATE THE DROID CHASES THE PLAYER
			delta_x = player.transform.position.x - transform.position.x;
			delta_y = player.transform.position.y - transform.position.y;		

			if(delta_x < contact && delta_y < contact){
				// The droid is touching the player.  The player thus looses Oxygen
				HealthScript.Affect_O2(O2_Drain);

				finished = true;
				startPosition = gameObject.transform.position.y; // reset this start position for the next call to move()
			}
			else{		
				movement.Set (-delta_x, delta_y, 0f);
				movement = movement.normalized * speed * Time.deltaTime;
				transform.Translate(movement);
			}
			//playerRigidbody.MovePosition (transform.position + movement);
		}
		else if (player.transform.position.x > (transform.position.x + headstart)) {
			// TRANSITION TO THE 'CHASING' STATE
			anim.SetBool ("DetectedPlayer", true);
			//Debug.Log ("Detected");
			chasing = true;
		} else {
			// DEFAULT STATE OF 'IDLING'
			//Debug.Log ("no seen");
			move ();  // move up and downn
		}			
	}

	void move()
	{
		time += Time.deltaTime * 2.0f;
		
		if (startPosition == -1) {
			startPosition = gameObject.transform.position.y;
		}
		
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, startPosition + Mathf.Sin(time) * moveSize, gameObject.transform.position.z);

	}
}
