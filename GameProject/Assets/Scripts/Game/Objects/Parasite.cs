using UnityEngine;
using System.Collections;

public class Parasite : MonoBehaviour {
	public float dragOnHost = 10.0f;	// Drag added to the host.
	public float thrashForce = 30.0f;	// Amount of force used when thrashing
	public float attachDuration = 5.0f;	// Length of time the parasite stays attached to the host
	private float timeAttached = 0.0f;	// Length of time the parasite has been attached to the current host.
	private GameObject host = null;		// Host that the parasite is currently attached to.

	private bool waitingToReattach = false;	// True if the parasite is waiting for the timer before re-attaching.
	private float reattachWaitDuration = 1.0f;		// Time after detaching before the parasite can reattach.
	private float reattachTimer = 0.0f;		// Amount of time elapsed since detaching from the last host.

	public float secondsBetweenThrashes = 1.0f;	// Number of seconds between thrashes
	private float thrashTimer = 0.0f;

	void Start() {
		timeAttached = 0.0f;
	}

	void Update() {
		if (host) {
			timeAttached += Time.deltaTime;
			if (timeAttached >= attachDuration) {
				DetachFromHost();
			}
		}

		if (waitingToReattach) {
			reattachTimer += Time.deltaTime;
			if (reattachTimer >= reattachWaitDuration) {
				waitingToReattach = false;
				reattachTimer = 0.0f;
			}
		}
	}

	void FixedUpdate() {
		// Apply the drag
		if (host && host.GetComponent<Rigidbody2D>()) {
			Vector2 pullDirection = host.GetComponent<Rigidbody2D>().velocity.normalized * -1.0f;
			host.GetComponent<Rigidbody2D>().AddForce(pullDirection * dragOnHost);

			thrashTimer += Time.deltaTime;
			if (thrashTimer >= secondsBetweenThrashes) {
				thrashTimer = 0.0f;
				Thrash();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		// @TODO Verify this is something we should attach to (like a player, enemy, etc.)
		AttachToHost(col.gameObject);
	}

	/// <summary>
	/// Attaches the parasite to a host.
	/// </summary>
	/// <param name="host">Host.</param>
	void AttachToHost(GameObject host) {
		// Prevent the parasite from re-attaching too quickly.
		if (waitingToReattach) {
			return;
		}

		this.host = host;
		transform.parent = host.transform;
		timeAttached = 0.0f;
		thrashTimer = 0.0f;
	}

	/// <summary>
	/// Detaches the parasite from its current host (if there is one);
	/// </summary>
	void DetachFromHost() {
		if (host) {
			transform.parent = null;
			host = null;
			timeAttached = 0.0f;
			thrashTimer = 0.0f;

			waitingToReattach = true;
			reattachTimer = 0.0f;
		}
	}

	/// <summary>
	/// Thrashes, pulling the host in a random direction.
	/// </summary>
	void Thrash() {
		Vector2 direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range (-1.0f, 1.0f));
		direction.Normalize ();

		host.GetComponent<Rigidbody2D>().AddForce(direction * thrashForce, ForceMode2D.Impulse);
	}
}
