using UnityEngine;
using System;
using System.Collections;

public class ParasiteMotion : MonoBehaviour {
	
	public float speed = 1.0f;     // speed of motion of the parasite
	public float range1 = 10.0f;   // parasite chases you if within this range
	public float range2 = 10.0f;   // parasite seizes you if within this range
	public float moveSize = 0.2f;  // for the up/down movement when Idling
	public float holdTime = 10.0f;  // duration of the grab

	Vector3 movement;
	Vector3 movement2;
	Animator anim;
	GameObject player;
	Vector2 PlayerThrustForce;
	DemoPlayerControl PlayerControlScript;
	float startTime = 0.0f; // The time when the parasite grabs the alien


	enum E {idling, chase, attack, hold, release};
	E state = E.idling;

	// Use this for initialization
	void Awake () {
		anim = GetComponent <Animator> ();
		anim.SetBool ("Seize", false);
		anim.SetBool ("Release", false);

		player = GameObject.FindWithTag("Player");	
		PlayerControlScript = player.GetComponent<DemoPlayerControl>();
	}
	
	// Update is called once per frame
	void Update () {
		float delta_x, delta_y, abs_x, abs_y;
		float rotation_y;

		if (player == null) // exit the update function if the player is dead
			return;

		// Determine the y rotation (if any) of this parasite
		// and remove it.  Put it back at the end of update()
		rotation_y = transform.rotation.eulerAngles.y;
		transform.Rotate(new Vector3(0.0f, -1*rotation_y, 0.0f));

		// Determine the distance betweeen the parasite and the character
		delta_x = player.transform.position.x - transform.position.x;
		delta_y = player.transform.position.y - transform.position.y;	

		abs_x = Math.Abs (delta_x); 
		abs_y = Math.Abs (delta_y);

		switch (state) {
		case E.idling:
			if (abs_x < range1 && abs_y < range1) {		
				state = E.chase;
			}
			break;
		case E.chase:
			if (abs_x < range2 && delta_y > -1 * range2 && delta_y < 0) {		
				anim.SetBool ("Seize", true); // Change Animation to Attack
				state = E.attack;
			}
			movement.Set (delta_x, delta_y, 0f);
			movement = movement.normalized * speed * Time.deltaTime;
			transform.Translate(movement);
			break;
		case E.attack:
			movement.Set (delta_x, delta_y, 0f);

			movement2 = movement.normalized * speed * 10 * Time.deltaTime;

			// Translate by the smallest betweeen movement and movement2
			if(movement2.magnitude < movement.magnitude)
			{
				transform.Translate(movement2);
			}
			else{
				transform.Translate(movement);
			}

			// Determine if the parasite is at the same location as the alien
			delta_x = player.transform.position.x - transform.position.x;
			delta_y = player.transform.position.y - transform.position.y;

			if(Math.Abs (delta_x) < 0.01f && Math.Abs (delta_y) < 0.01f)
			{// Switch the state to Hold and Change the Player's Thrust Force
				state = E.hold;
				startTime = Time.time;
				//Debug.Log (string.Format ("startTime!!!!! {0}", startTime));
				PlayerThrustForce = PlayerControlScript.thrustForce;
				PlayerControlScript.thrustForce = new Vector2(PlayerThrustForce.x/-5.0f, PlayerThrustForce.y/7.0f);
			}
			break;
		case E.hold:
			movement.Set (delta_x, delta_y, 0f);
			transform.Translate(movement);

			// Go to state Release once holdTime has elapsed
			if(Time.time > startTime + holdTime)
			{
				state = E.release;
				anim.SetBool ("Seize", false);
				anim.SetBool ("Release", true);
				PlayerControlScript.thrustForce = PlayerThrustForce; // Set the thrust back to it's origina value
			}
			break;
		case E.release:

			break;
		}

		Debug.Log (string.Format ("dx {0}, start {1}, time {2}, state {3}", delta_x, startTime, Time.time, state));

		// Put back the rotation of the parasite now that it has moved
		transform.Rotate(new Vector3(0.0f, rotation_y, 0.0f));

		//movement.Set (0.3f, 0.3f, 0f);
		//transform.Translate(movement);
	}
}





