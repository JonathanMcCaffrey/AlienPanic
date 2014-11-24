using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public int maxHealth = 1;
	private int healthRemaining = 1;

	// Use this for initialization
	void Start () {
		healthRemaining = maxHealth;	
	}

	/// <summary>
	///  Changes the current health of the object.
	/// </summary>
	/// <param name="healthAdjustment">Amount to adjust the health.</param>
	public void ChangeHealth(int healthAdjustment) {
		healthRemaining += healthAdjustment;
		healthRemaining = Mathf.Clamp(healthRemaining, 0, maxHealth);

		if (healthRemaining == 0) {
			OnDeath ();
		}
	}

	/// <summary>
	/// Callback triggered when the game object runs out of health. Override in subclasses to show explosions, make the entity disappear, etc.
	/// </summary>
	protected virtual void OnDeath() {
		Debug.Log (string.Format ("{0} has died.", gameObject.name));
	}
}
