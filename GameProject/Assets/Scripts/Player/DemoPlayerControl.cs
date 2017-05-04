using UnityEngine;
using System.Collections;

public class DemoPlayerControl : MonoBehaviour{
	bool movingUp = false;		// Flag to indicate if the player is in a moving-up state.
	float particleTimer = 0;	// Timer used to delay when the particles start / stop emitting.
	
	public ParticleSystem particleEngine = null;		// Particle engine used for the spaceship's thruster.
	public Vector3 maxSpeed = new Vector2(5.0f, 5.0f);	// Maximum speed for each dimension the player can travel in. Min speed is the negative of the max. Stored as a Vector2 for more convenient editing. 
	public float levelCeiling = 19.5f;					// Max height / ceiling for the level. Used instead of lining the level with colliders.
	public Vector2 thrustForce = new Vector2(10.0f, 200.0f); // Thrust forces that get applied to the ship. Horizontal force is always applied, vertical force is applied when the player is holding the ascend button.	
	
	/// <summary>
	/// Fixed update. Called a fixed number of times per second.
	/// </summary>
	void FixedUpdate()
	{
		particleTimer += Time.deltaTime;
		
		if (gameObject.transform.position.y < levelCeiling) {
			GetComponent<Rigidbody2D>().AddForce (new Vector2 (thrustForce.x, 0), ForceMode2D.Force);	// Add the horizontal force.
			
			if (movingUp) {
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, thrustForce.y), ForceMode2D.Force);	// Add the vertical force.
				
				if (particleTimer > 0.02f || (gameObject.transform.position.y > 10 && particleTimer > 0.01f)) {
					particleTimer = 0.0f;
					particleEngine.Emit (1);
				}
			} else {
				if (particleTimer > 0.3f || (gameObject.transform.position.y > 10 && particleTimer > 0.15f)) {
					particleTimer = 0.0f;
					particleEngine.Emit (1);
				}
			}
		}



		// Ensure the player doesn't exceed the velocity constraints.
		GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Clamp(GetComponent<Rigidbody2D>().velocity.x, -maxSpeed.x, maxSpeed.x), Mathf.Clamp(GetComponent<Rigidbody2D>().velocity.y, -maxSpeed.y, maxSpeed.y));
	}
	
	/// <summary>
	/// Update function. Called once per game tick.
	/// </summary>
	void Update()
	{
		if (Input.GetButtonDown ("Thrust")) {
			movingUp = true;	
		}
		else if (Input.GetButtonUp ("Thrust")) {
			movingUp = false;
		}
	}
	
}