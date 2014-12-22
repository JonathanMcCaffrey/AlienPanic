using UnityEngine;
using System.Collections;

public class ButtonPause : MonoBehaviour {
	public GameObject defaultPauseMenu = null;
	
	public void onClick() {
		//TODO Pause game logic
		
		var pauseMenu = GameObject.Instantiate (defaultPauseMenu) as GameObject;
		pauseMenu.transform.parent = gameObject.transform.parent;
		pauseMenu.transform.localScale = Vector3.one;
		pauseMenu.transform.localPosition = Vector3.zero;
	}
}
