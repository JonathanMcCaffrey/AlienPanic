/*
 *  StateGamePaused.cs
 *	
 *	
 *
 */

using UnityEngine;
using System.Collections;

public class StateGamePaused : GameState {
	
	
	// Paused GUI
	public override void StateGUI() {
		
		// Render the GUI.
		GUI.Label (new Rect(20, 10, 150, 20), string.Format ("state: GAME PAUSED"));
	}
	
	public override void StateUpdate() {
		//print ("StateGamePaused::StateUpdate() ");

		// Stop the spacetime
		Time.timeScale = 0;
	}	
}
