using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public static MonoBehaviour instance = null;


	void Awake() {
		if (instance) {
			//Destroy (gameObject);
		} else {

			/*Debug.Log("Additional Player Deleted");

			DontDestroyOnLoad(gameObject);*/

			instance = this;


		}
	}
	
}
