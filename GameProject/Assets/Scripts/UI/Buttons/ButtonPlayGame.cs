using UnityEngine;
using System.Collections;

public class ButtonPlayGame : MonoBehaviour {
	public GameObject mainMenu = null;
	
	
	public void onClick() {

		GameManager.instance.NewGameState(GameManager.instance.stateNewGame);

		//Application.LoadLevel ("Level_1");
		//TODO Goto first level. replace later with goto level select
	}
}
