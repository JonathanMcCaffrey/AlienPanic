using UnityEngine;
using System.Collections;

public class DemoBirdController : MonoBehaviour {

	//Editor UI Elements
	public GameObject birdsPrefab;
	[Range(0.01f, 0.10f)]
	public float birdsFrequency;
	[Range(1, 10)]
	public int numberOfBirdsInGroup;
	[Range(0.1f, 1.0f)]
	public float birdsMovementSpeed;
	[Range(1, 5)]
	public int birdsRelaxPeriod;

	//Internal variables
	private int birdsPassedCounter = 0;
	private ArrayList birdsArray = new ArrayList();

	void Awake()
	{
		Random.seed = (int)Time.time;
	}

	// Update is called once per frame
	void Update () 
	{
		createBirds();
		updateBirds();
		destroyBirds();
	}

	void createBirds(){
		float freq = (float)birdsFrequency;
		float randVal = Random.value;
		if(randVal < freq && birdsRelaxPeriod > birdsPassedCounter)
		{
			DemoBird bird = new DemoBird(birdsPrefab, gameObject.name);
			birdsArray.Add(bird);
			birdsPassedCounter++;
		}
	}

	void updateBirds()
	{
		for(int i=0; i < birdsArray.Count; i++)
		{
			(birdsArray[i] as DemoBird).updateBirdMovement(birdsMovementSpeed);
		}
	}

	void destroyBirds()
	{
		for(int i=0; i < birdsArray.Count; i++)
		{
			(birdsArray[i] as DemoBird).destroyBird();
			if((birdsArray[i] as DemoBird).isBirdDestroyed)
				birdsArray.RemoveAt(i);
		}		
	}
}




