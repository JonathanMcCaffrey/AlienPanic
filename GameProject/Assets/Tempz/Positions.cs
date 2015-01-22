using UnityEngine;
using System.Collections;

public class Positions : MonoBehaviour {

	void OnDrawGizmos() {
		Gizmos.DrawWireSphere (transform.position, 1);
	}

}
