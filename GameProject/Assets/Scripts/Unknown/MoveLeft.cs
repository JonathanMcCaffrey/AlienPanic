using UnityEngine;
using System.Collections;

public class MoveLeft : MonoBehaviour
{
	public float speed;
	
	void Start ()
	{
		GetComponent<Rigidbody2D>().velocity = (-1) * (transform.right * speed);
	}
}
