using UnityEngine;
using System.Collections;

public class DemoMissle : MonoBehaviour {
	
	public GameObject player = null;

	void Start() {
		GameObject foundPlayer = GameObject.Find ("Character");

		if (foundPlayer) {
			player = foundPlayer;
		}
	}

	private bool once = true;
	void Update () {
		if (!once) {
			if(!GetComponent<Renderer>().isVisible) {
				Destroy(gameObject);
			}
		}
	}
	
	void OnWillRenderObject() {
		if (once) {
			float diff = (Mathf.Sin(Time.time) * 3.0f) - 1.5f;
			
			transform.position = new Vector3 (transform.position.x, player.transform.position.y + diff + 1.0f, -1);
			GetComponent<Rigidbody2D>().AddForce (new Vector2 (-30, 0), ForceMode2D.Force);
			once = false;
		}
	}
	
	void OnCollisionEnter2D (Collision2D col) {
		Health colliderHealth = col.gameObject.GetComponent<Health>();
		if (colliderHealth) {
			colliderHealth.ChangeHealth(-1);
		}

		if (col.gameObject.tag == "Player") {
		//	col.transform.localScale = new Vector3 (0f, 0f, 1);
			Destroy (gameObject);
		}
	}
}
