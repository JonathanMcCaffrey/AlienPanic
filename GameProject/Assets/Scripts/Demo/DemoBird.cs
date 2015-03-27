using UnityEngine;
using System.Collections;

public class DemoBird {

	private GameObject bird;
	//private bool hasRendered = false;
	public bool isBirdDestroyed = false;

	//Should not be used.
	public DemoBird(){
		//Unity is not allowing to remove this constructor.
		//Dont want to load the prefab using Resouces.Load because then the directory structure would need to be changed.
		bird = null;
	}

	public DemoBird(GameObject birdPrefab, string birdsContainer){
		createBird(birdPrefab, birdsContainer);
	}

	private void createBird(GameObject birdPrefab, string birdsContainer){
		bird = GameObject.Instantiate(birdPrefab) as GameObject;

		//Add it inside the parent container
		if(birdsContainer != null)
			bird.transform.parent = GameObject.Find(birdsContainer).transform;

		bird.transform.position = new Vector2(Random.Range(-5,5), Random.Range(10,20));

		Debug.Log("Bird Created");
	}

	public void updateBirdMovement(float birdSpeed)
	{
		bird.transform.position = new Vector2((float)(bird.transform.position.x - birdSpeed), bird.transform.position.y);
		//if(bird.renderer.isVisible)
			//hasRendered = true;
	}

	public void destroyBird()
	{
		//Get the camera bounds to destroy if the object is outside the camera bounds.
		Bounds currentBounds = bounds(Camera.main.GetComponent<Camera>());

		//Destory if offScreen
		//if((!(bird.renderer.isVisible) && hasRendered == true))
		if(bird.transform.position.x <= (currentBounds.center.x - currentBounds.extents.x))
		{
			GameObject.Destroy(bird);
			isBirdDestroyed = true;
			Debug.Log("Bird Destroyed");
		}
	}

	//This needs to be removed from here to a sepearte class.
	private Bounds bounds(Camera camera)
	{
		//Fill the default main camera
		if(camera == null)
			camera = Camera.main.GetComponent<Camera>();
		
		float screenAspect = (float)Screen.width / (float)Screen.height;
		float cameraHeight = Camera.main.GetComponent<Camera>().orthographicSize * 2;
		Bounds bounds = new Bounds(Camera.main.GetComponent<Camera>().transform.position,
		                           new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
		
		return bounds;
	}
}




