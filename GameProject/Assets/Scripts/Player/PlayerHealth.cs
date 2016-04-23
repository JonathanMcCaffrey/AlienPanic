using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerHealth : Health {

	public GameObject explosionPrefab;
	public int deplete_rate; // Rate at which Oxygen (O2) is depleted normally
	public int modify_rate;  // Rate at which Oxygen (O2) is lost/gain in special circumstances

	float O2_Current = 100.0f;
	float O2_Max = 100.0f;
	float O2_Min = 0.0f;

	float O2_Modifier = 0.0f;  // Increased by Tokens and Decreased by droid or other bad things

	Slider OxygenSlider;
	Image OxygenFill;

	Color MaxO2Color = Color.white;
	Color MinO2Color = Color.red;

	void Start() {
		if (!explosionPrefab) {
			Debug.LogError("Unable to set up player health: No explosion prefab is attached.");
		}

		OxygenSlider = GameObject.Find ("O2Slider").GetComponent<Slider> ();
		OxygenFill = GameObject.Find ("O2Fill").GetComponent<UnityEngine.UI.Image> ();
	}

	void Update () {
		// Update the value of O2_Current
		Set_Current_O2 ();  

		// Update the Oxygen Slier (Heath Bar) and set its color/size 
		Update_Oxygen_Slier ();

		// Game Over if there is no more Oxygen
		if (O2_Current <= O2_Min) {  
			GameOver ();
		}
	}

	
	void Set_Current_O2()
	{
		float O2_Change = 0.0f;
		
		// Determine how much O2 is changing
		if (O2_Modifier > 0.0f){
			// Oxygen is gained at the rapid rate
			O2_Change = Time.deltaTime * modify_rate;
			O2_Modifier = O2_Modifier - O2_Change;
			if(O2_Modifier < 0.0f)
				O2_Modifier = 0.0f;
		}
		else if(O2_Modifier < 0.0f){
			// Oxygen is lost at the rapid rate
			O2_Change = -1*Time.deltaTime * modify_rate;
			O2_Modifier = O2_Modifier - O2_Change;
			if(O2_Modifier > 0.0f)
				O2_Modifier = 0.0f;
		}
		else {
			// Oxygen Depletes at a normal rate
			O2_Change = -1*Time.deltaTime * deplete_rate;
		}
		
		// Set the current level of O2
		O2_Current = O2_Current + O2_Change;
		if(O2_Current > O2_Max)
			O2_Current = O2_Max;

		// Stop increasing O2 if it is already at Maximum
		if (O2_Current == O2_Max && O2_Modifier > 0.0f)
			O2_Modifier = 0.0f;
	}
	
	void Update_Oxygen_Slier(){
		float O2_Size;               // O2_Size and O2_Rate are for the scaling of the
		float O2_Size_Rate = 5.0f;   // Oxygen bar when this one is running low.
				
		OxygenSlider.value = O2_Current;
		OxygenSlider.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		
		// Display the Oxygen Bar in red and change the size 
		// rapidly if the Oxygen is really low
		if (O2_Current > O2_Max / 2) {
			OxygenFill.color = Color.white;
			
		} else {
			OxygenFill.color = Color.Lerp (MinO2Color, MaxO2Color, (float)O2_Current / (O2_Max/2));
			
			// Change the size of the Oxygen Bar when it's under 25%
			if(O2_Current < O2_Max / 4) {
				O2_Size = (float)(Math.Sin(Time.time*O2_Size_Rate)*Math.Sin(Time.time*O2_Size_Rate));
				OxygenSlider.transform.localScale = new Vector3(1.0f + O2_Size*0.8f, 1.0f + O2_Size*0.14f, 1.0f);
			}
		}
	}

	// Called from other GameObjects to change O2_Modifier
	public void Affect_O2(float O2)
	{
		O2_Modifier += O2;
	}



	/// <summary>
	/// Callback triggered when the game object runs out of health. Override in subclasses to show explosiosn, make the entity disappear, etc.
	/// </summary>
	protected override void OnDeath() {
		GameObject explosion = (GameObject)GameObject.Instantiate(explosionPrefab);
		explosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		
		GameObject.Destroy(gameObject);
		GameOver ();
	}

	void GameOver()
	{
		if (GameManager.instance) {
			GameManager.instance.ChangeState(GameManager.instance.stateGameLost);
		}
	}
}
