using UnityEngine;
using System.Collections;

public class PlayerHealth : Health {

	/// <summary>
	/// Callback triggered when the game object runs out of health. Override in subclasses to show explosiosn, make the entity disappear, etc.
	/// </summary>
	protected override void OnDeath() {
		GameObject.Destroy(gameObject);
	}
}
