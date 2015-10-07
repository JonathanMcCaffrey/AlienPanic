/*
 * 	CameraTracking.cs
 * 	
 * Handles the Camera to player tracking, and the vertical motion of the camera between quadrants. 
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraTracking : MonoBehaviour {
	
	public GameObject defaultQuadrant = null;
	public GameObject quadrantContainer = null;
	public bool isVisible = false;
	public bool isEditorGuide = false;
	
	
	[Range(0.01f, 1.0f)]
	public float distanceThreshold = 0;

	//TODO Add Quadrant Size
	
	public float startY = 0;
	
	//TODO Add this to editor interface?
	public float cameraOffsetY = 1.49f;
	public float cameraOffsetX = 6.2f;
	
	
	public List<GameObject> quadrants = new List<GameObject>();
	
	//TODO Generate this on start, based on the current player height
	private int currentQuadrant = 0;
	private int CurrentQuadrant {
		get {
			if(currentQuadrant > quadrants.Count - 1) { currentQuadrant = quadrants.Count - 1; }
			if(currentQuadrant < 0) { currentQuadrant = 0; }
			
			return currentQuadrant;
		}
	}
	
	Transform getQuadTransform(int quadrant) {
		if (quadrant > quadrants.Count - 1) {
			return null;
		}
		
		return quadrants [quadrant].transform;
	}
	
	float QuadHeight() { return defaultQuadrant ? defaultQuadrant.GetComponent<BoxCollider2D>().size.y : 0; } 
	float ThresholdHeight() {  return QuadHeight() * distanceThreshold; }
	
	
	public GameObject thresholdDisplay = null;
	
	GameObject GetPlayer() { return Player.instance ? Player.instance.gameObject : null; }
	float PlayerY() { return GetPlayer() ? GetPlayer ().transform.position.y : 0; }
	float PlayerX() { return GetPlayer() ? GetPlayer ().transform.position.x : 0; }
	
	GameObject GetCamera() { return GameCamera.instance ? GameCamera.instance.gameObject : null; }
	float CameraY() { return GetCamera ().transform.position.y; }
	float CameraX() { return GetCamera ().transform.position.x; }
	void SetCameraY(float y) { 
		if (GetCamera ()) {
			GetCamera().transform.position = new Vector3(GetCamera ().transform.position.x, y, GetCamera ().transform.position.z);
		}
	}
	void SetCameraX(float x) { 
		if (GetCamera ()) {
			GetCamera().transform.position = new Vector3(x, GetCamera ().transform.position.y, GetCamera ().transform.position.z);
		}
	}
	
	
	float getQuadMin(int quad) {
		return getQuadTransform (quad).position.y + ThresholdHeight () / 2.0f - QuadHeight() / 2.0f;
	}
	float getQuadMax(int quad) {
		return QuadHeight () + getQuadTransform (quad).position.y - ThresholdHeight () / 2.0f - QuadHeight() / 2.0f;
	}
	
	void TransitionUp (ref bool isTransitioning) {
		if (CurrentQuadrant < quadrants.Count - 1) {
			if (PlayerY () > getQuadMax (CurrentQuadrant)) {
				var thresholdDistance = getQuadMin (CurrentQuadrant + 1) - getQuadMax (CurrentQuadrant);
				var playerDistance = PlayerY () - getQuadMax (CurrentQuadrant);
				var delta = playerDistance / thresholdDistance;
				if (delta >= 1.0f) {
					currentQuadrant++;
					return;
				}
				SetCameraY (CurrentQuadrant * QuadHeight () + QuadHeight () * delta + cameraOffsetY);
				isTransitioning = true;
			}
		}
	}	
	
	void TransitionDown (ref bool isTransitioning) {
		if (CurrentQuadrant > 0) {
			if (PlayerY () < getQuadMin (CurrentQuadrant)) {
				var thresholdDistance = getQuadMax (CurrentQuadrant - 1) - getQuadMin (CurrentQuadrant);
				var playerDistance = PlayerY () - getQuadMin (CurrentQuadrant);
				var delta = playerDistance / thresholdDistance;
				if (delta >= 1.0f) {
					currentQuadrant--;
					return;
				}
				SetCameraY (CurrentQuadrant * QuadHeight () - QuadHeight () * delta + cameraOffsetY);
				isTransitioning = true;
			}
		}
	}
	
	void Update () {
		if (GetPlayer ()) {
			SetCameraX(PlayerX() + cameraOffsetX);
			
			//TODO Bézier this? 
			bool isTransitioning = false;
			TransitionUp (ref isTransitioning);
			TransitionDown (ref isTransitioning);
			if(isTransitioning) { return; } 
			
			SetCameraY(CurrentQuadrant * QuadHeight() + cameraOffsetY);
		}
	}
	
	void RefreshQuadrantDisplay () {
		var quadList = quadrantContainer.GetComponentsInChildren<CameraQuadrant> ();
		quadrants.Clear ();
		float quadIndex = 0;
		float quadCount = quadList.Length;
		
		foreach (var quad in quadList) {
			if (quadrants.Contains (quad.gameObject)) {
				continue;
			}
			quadrants.Add (quad.gameObject);
			float yVal = quadIndex * QuadHeight() + startY;
			quad.gameObject.transform.position = new Vector3 (0, yVal, 5000);
			quad.transform.localScale = new Vector3 (isEditorGuide ? 5000 : 1, 1, 1);
			quadIndex++;
			
			if(!isVisible) {
				quad.gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
			} else if(isEditorGuide) {
				quad.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, quadIndex / quadCount, (quadCount - quadIndex) / quadCount,0.8f);
			} else {
				quad.gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0.8f,0.8f,0.8f);
			}
		}
	}
	
	void RefreshThresholdDisplay () {
		if (thresholdDisplay) {
			if (quadrants.Count >= 2) {
				var y = (quadrants [0].transform.localPosition.y + quadrants [1].transform.localPosition.y) / 2.0f;
				thresholdDisplay.transform.position = new Vector3 (0, y, 5000);
				thresholdDisplay.transform.localScale = new Vector3 (isEditorGuide ? 5000 : 1, distanceThreshold, 1);
				thresholdDisplay.SetActive (true);
				
				if(isEditorGuide || !isVisible) {
					thresholdDisplay.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
				} else {
					thresholdDisplay.GetComponent<SpriteRenderer>().color = new Color(0,0.8f,0.8f,1);
				}
			}
			else {
				thresholdDisplay.SetActive (false);
			}
		}
	}
	
	//TODO Add debug wrapper
	public void OnDrawGizmos() {
		if (defaultQuadrant) {
			RefreshQuadrantDisplay ();
			RefreshThresholdDisplay ();
		}
	}
}
