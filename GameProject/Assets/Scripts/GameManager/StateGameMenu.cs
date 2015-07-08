using UnityEngine;
using System.Collections;

public class StateGameMenu : GameState {
	
	public override void StateGUI() {
		GUILayout.Label ("state: MAIN MENU");
	}

	public override void StateUpdate() {
		print ("StateGameMenu::StateUpdate() ");
		Application.LoadLevel("_MainMenu");
		
		GameManager.instance.ChangeState(GameManager.instance.stateGameMenu);
		
	}
}
