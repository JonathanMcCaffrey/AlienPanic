using UnityEngine;
using System.Collections;

public class ResetGame : MonoBehaviour 
{
	public void DoSomething()
	{
		// TODO: Prepare the game for a reset or restart
		// InitialiseVariables() //currently not required because each scene creates a new PLAYER OBJECT (and friends). 
		// PLAYER OBJECT should be created here and passed along or persisted to the GAME LEVEL
		// BuldLevel() //Generate or load from XML
		// In the distant future the game should be started by changing the gameState of the PLAYER OBJECT
		
		Debug.Log ("Loading scene \"Example_2_Corridor\"...");
		Application.LoadLevel ("Example_2_Corridor");
		
	}
	
}