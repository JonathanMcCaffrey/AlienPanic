using UnityEngine;
using System.Collections;

public class DemoCollectable : MonoBehaviour {
	
	private bool once = true;

	void OnWillRenderObject() {
		once = true;
	}
	
	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Player") {
			gameObject.transform.localScale = new Vector3 (0f, 0f, 1);
			Score.instance.AddPoint (10);
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
			if(!renderer.isVisible) {
				Destroy(gameObject);
			}
		}
	}
}
