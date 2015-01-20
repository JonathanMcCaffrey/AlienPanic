/* ExplodeOnContact.cs
 * 
 * Display specified explosion. Destroys this object without concequences.
 * Does not harm the player or affect the score.
 * 
 */


using UnityEngine;
using System.Collections;

public class ExplodeOnContact : MonoBehaviour
{
	public GameObject explosion;
	
	void OnCollisionEnter2D (Collision2D other) {
		print("DestroyByContact::OnCollisionEnter2D() " + other.gameObject.name);

		if (other.gameObject.tag == "Player") {
			Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
		}

		if (other.gameObject.tag == "Wall") {
			Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
		}
		Destroy (gameObject);
	}
}