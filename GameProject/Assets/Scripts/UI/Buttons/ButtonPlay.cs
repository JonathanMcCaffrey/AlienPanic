using UnityEngine;
using System.Collections;

public class ButtonPlay : MonoBehaviour {
	public GameObject pauseMenu = null;
	
	public void onClick() {
		Destroy (pauseMenu);
	}
}
