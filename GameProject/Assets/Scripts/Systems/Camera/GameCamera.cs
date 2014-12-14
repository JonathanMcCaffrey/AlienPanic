using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCamera : MonoBehaviour {
	public static MonoBehaviour instance = null;
	void Awake() {
		if (instance) {
			Destroy (gameObject);
		} else {
			Debug.Log("Additional Game Camera Deleted");
			
			DontDestroyOnLoad(gameObject);
			instance = this;
		}
	}
}
