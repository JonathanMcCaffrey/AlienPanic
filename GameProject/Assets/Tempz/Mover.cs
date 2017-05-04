using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
	public float speed;

	void Start ()
	{
		GetComponent<Rigidbody2D>().velocity = transform.right * speed;
	}
}
