using UnityEngine;
using System.Collections;

public class ButtonRetry : MonoBehaviour {

	public void onClick() {
		Application.LoadLevel ("Level_1");
		//TODO Reset Logic
	}
}
