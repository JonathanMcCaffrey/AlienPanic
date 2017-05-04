using UnityEngine;
using System.Collections;

public class DemoCollectable : MonoBehaviour {

	public float O2_Bonus; // The Oxygen gained from collecting this token

	bool once = true;
	GameObject player;
	PlayerHealth HealthScript;

	void OnWillRenderObject() {
		once = true;

	}

	void Start () {
		player = GameObject.FindWithTag ("Player");	
		HealthScript = player.GetComponent<PlayerHealth>();
	}
		
	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Player") {
			gameObject.transform.localScale = new Vector3 (0f, 0f, 1);

			// Add Oxygen to the player's Oxygen level
			HealthScript.Affect_O2(O2_Bonus);
		
			if(Score.instance) {
				Score.instance.AddPoint (10);
			}

			Destroy (gameObject);
		}
	}

	public float moveSize = 1;
	float time = 0;
	float speed = 5;
	float startPosition = -1;
	
	void Update () {
		time += Time.deltaTime * speed;
		
		if (startPosition == -1) {
			startPosition = gameObject.transform.position.y;
		}
		
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, startPosition + Mathf.Sin(time) * moveSize, gameObject.transform.position.z);

		if (!once) {
			if(!GetComponent<Renderer>().isVisible) {
				Destroy(gameObject);
			}
		}
	}
}
