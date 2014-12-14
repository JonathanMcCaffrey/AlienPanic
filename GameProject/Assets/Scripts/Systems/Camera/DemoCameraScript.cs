using UnityEngine;
using System.Collections;

public class DemoCameraScript : MonoBehaviour {
	
	public GameObject mainPlayer = null;
	
	void Update () {
		if (mainPlayer && mainPlayer.transform.localScale.x > 0) {
			float yPos = (mainPlayer.transform.position.y - 3.5f) * 1.55f;
			gameObject.transform.position = new Vector3 (mainPlayer.transform.position.x + 6.15f, yPos, - 20);
			
			float limitBot = 0;
			if (gameObject.transform.position.y < limitBot) {
				gameObject.transform.position = new Vector3 (gameObject.transform.position.x, limitBot, gameObject.transform.position.z);
			}
			
			float limitTop = 15;
			if (gameObject.transform.position.y > limitTop) {
				gameObject.transform.position = new Vector3 (gameObject.transform.position.x, limitTop, gameObject.transform.position.z);
			}
		}
	}
}
