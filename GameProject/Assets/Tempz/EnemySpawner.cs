using UnityEngine;
using System.Collections;

// Formation Controller

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 10;
	public float height = 5;
	public float speed = 5.0f;
	public float padding = 1;

	private int direction = 1;
	private float boundaryTopEdge, boundaryBottomEdge;



	void Start () {

		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;

		boundaryTopEdge = camera.ViewportToWorldPoint(new Vector3(1,1,distance)).y + padding;
		boundaryBottomEdge = camera.ViewportToWorldPoint(new Vector3(0,0,distance)).y - padding;


		foreach (Transform child in transform) {
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child; 
		}
	
	}
	void OnDrawGizmos() {

		//
		// Draw a box for the editor around the enemy spawner positions
		//

		float xmin, xmax, ymin, ymax;
		xmin = transform.position.x - 0.5f * height;
		xmax = transform.position.x + 0.5f * height;
		ymin = transform.position.y - 0.5f * height;
		ymax = transform.position.y + 0.5f * height;

		Gizmos.DrawLine(new Vector3(xmin, ymin, 0), new Vector3(xmin, ymax, 0));
		Gizmos.DrawLine(new Vector3(xmin, ymax, 0), new Vector3(xmax, ymax, 0));
		Gizmos.DrawLine(new Vector3(xmax, ymax, 0), new Vector3(xmax, ymin, 0));
		Gizmos.DrawLine(new Vector3(xmax, ymin, 0), new Vector3(xmin, ymin, 0));
	}


	void Update () {
	
	}
}
