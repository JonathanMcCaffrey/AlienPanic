using UnityEngine;
using System.Collections;

public class ButtonRetry : MonoBehaviour {

	public void onClick() {
		Application.LoadLevel ("Example_1_Graybox");
		//TODO Reset Logic
	}
}
