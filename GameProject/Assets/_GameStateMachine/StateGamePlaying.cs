/*
 *  StateGamePlaying.cs
 *	
 *	
 *
 */

using UnityEngine;
using System.Collections;

public class StateGamePlaying : GameState {

	bool movingUp = false;		// Flag to indicate if the player is in a moving-up state.
	float particleTimer = 0;	// Timer used to delay when the particles start / stop emitting.
	
	public ParticleSystem particleEngine = null;		// Particle engine used for the spaceship's thruster.
	public Vector3 maxSpeed = new Vector2(5.0f, 5.0f);	// Maximum speed for each dimension the player can travel in. Min speed is the negative of the max. Stored as a Vector2 for more convenient editing. 
	public float levelCeiling = 19.5f;					// Max height / ceiling for the level. Used instead of lining the level with colliders.
	public Vector2 thrustForce = new Vector2(10.0f, 200.0f); // Thrust forces that get applied to the ship. Horizontal force is always applied, vertical force is applied when the player is holding the ascend button.	



	//TODO: Draw the StateGamePlaying GUI here
	public override void StateGUI() {

		GUILayout.Label("state: GAME PLAYING");

	}

	public override void StateUpdate() {

		// print ("StateGamePlaying::StateUpdate() ");

		//TODO: write ProcessGameFlowInput() //Reset or next level
		//TODO: write UpdateContinuousGameState() //moving objects here, AI, space junk to avoid
		//TODO: update ProcessGameplayInput(), detatch particle stuff from player movement (maybe --a)
	
		// Handle input from the player here
		ProcessGameplayInput();
	}

	//TODO: Trigger events? Maybe later...
	void  OnTriggerEnter2D (Collider2D other) {
		print ("StateGamePlaying::OnTriggerEnter2D() ");
		//Destroy(other.gameObject);
	
	}

	//TODO: Collide with other stuff here
	void  OnCollisionEnter2D (Collision2D other) {
		print ("StateGamePlaying::OnCollisionEnter2D() ");

		if (other.gameObject.tag == "LevelWin")
			gameManager.NewGameState (gameManager.stateGameWon);
	}

	//I combined this is from DemoCharacterController.cs , might need a fix. Looks ok to me --andre
	void ProcessGameplayInput() {
		
		if (Input.GetButtonDown ("Thrust")) {
			movingUp = true;	
		}
		else if (Input.GetButtonUp ("Thrust")) {
			movingUp = false;
		}
		
		particleTimer += Time.deltaTime;
		
		if (gameObject.transform.position.y < levelCeiling) {
			rigidbody2D.AddForce (new Vector2 (thrustForce.x, 0), ForceMode2D.Force);	// Add the horizontal force.
			
			if (movingUp) {
				rigidbody2D.AddForce (new Vector2 (0, thrustForce.y), ForceMode2D.Force);	// Add the vertical force.
				
				if (particleTimer > 0.02f || (gameObject.transform.position.y > 10 && particleTimer > 0.01f)) {
					particleTimer = 0.0f;
					if (particleEngine != null) particleEngine.Emit (1);
				}
			} else {
				if (particleTimer > 0.3f || (gameObject.transform.position.y > 10 && particleTimer > 0.15f)) {
					particleTimer = 0.0f;
					if (particleEngine != null) particleEngine.Emit (1);
				}
			}
		}
		
		// Ensure the player doesn't exceed the velocity constraints.
		rigidbody2D.velocity = new Vector2(Mathf.Clamp(rigidbody2D.velocity.x, -maxSpeed.x, maxSpeed.x), Mathf.Clamp(rigidbody2D.velocity.y, -maxSpeed.y, maxSpeed.y));
	}
}
