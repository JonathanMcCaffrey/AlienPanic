using UnityEngine;
using System.Collections;

public class ButtonResume : MonoBehaviour {
	public GameObject pauseMenu = null;
	
	public void onClick() {
		Destroy (pauseMenu);
	}
}
