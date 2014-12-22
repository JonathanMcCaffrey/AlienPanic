using UnityEngine;
using System.Collections;

public class ButtonPlayGame : MonoBehaviour {
	public GameObject mainMenu = null;
	
	
	public void onClick() {
		Application.LoadLevel ("Example_1_Graybox");
		//TODO Goto first level. replace later with goto level select
	}
}
