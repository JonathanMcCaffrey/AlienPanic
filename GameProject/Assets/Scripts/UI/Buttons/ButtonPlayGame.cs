using UnityEngine;
using System.Collections;

public class ButtonPlayGame : MonoBehaviour {

	public void onClick() {
		//TODO Goto first level. replace later with goto level select
		GameManager.instance.NewGameState(GameManager.instance.stateNewGame);
	}
}
