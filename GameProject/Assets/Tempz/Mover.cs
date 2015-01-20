using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
	public float speed;

	void Start ()
	{
		rigidbody2D.velocity = transform.right * speed;
	}
}
