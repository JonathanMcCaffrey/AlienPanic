using UnityEngine;
using System.Collections;

public class DemoPlayerControl : MonoBehaviour {
	bool movingUp = false;
	bool keyTouched = false;
	
	public ParticleSystem particleEngine = null;
	
	float particleTimer = 0;
	
	void FixedUpdate() {
		particleTimer += Time.deltaTime;
		
		if (gameObject.transform.position.y < 19.5) {
			
			if (movingUp) {
				rigidbody2D.velocity = Vector2.zero;
				rigidbody2D.AddForce (new Vector2 (275, 200), ForceMode2D.Force);
				
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
		
	}
	
	void Update() {
		
		if (Input.GetKeyDown (KeyCode.Space)) {
			movingUp = true;
			keyTouched = true;
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			movingUp = false;
		}
	}
}