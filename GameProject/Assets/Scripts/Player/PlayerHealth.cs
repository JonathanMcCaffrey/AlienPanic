using UnityEngine;
using System.Collections;

public class PlayerHealth : Health {
	public GameObject explosionPrefab;

	void Start() {
		if (!explosionPrefab) {
			Debug.LogError("Unable to set up player health: No explosion prefab is attached.");
		}
	}

	/// <summary>
	/// Callback triggered when the game object runs out of health. Override in subclasses to show explosiosn, make the entity disappear, etc.
	/// </summary>
	protected override void OnDeath() {
		GameObject explosion = (GameObject)GameObject.Instantiate(explosionPrefab);
		explosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		
		GameObject.Destroy(gameObject);

		if (GameManager.instance) {
			GameManager.instance.ChangeState(GameManager.instance.stateGameLost);
		}
	}
}
