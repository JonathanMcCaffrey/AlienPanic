using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO Wrap this since it will probably break mobile
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;


public class AssetPlacementPositionSystem : MonoBehaviour {
	//TODO Make this work in 3D?
	public float xPosition = 0;
	public float yPosition = 0;

	//TODO Find some way to auto fix the position in relation to the mouse
	public float adjustX = 0;
	public float adjustY = 0;

	public static Vector3 selectedPosition = Vector3.zero;

	//TODO Cool shader effect for placing assets? (Might appear laggy in editor)
	//bool shouldShowCoolShaderEffectOverlayForPlacingAssetsWithinTheEditor = true;

	//TODO Auto create marker rather than setting it. Make private
	//TODO Make the asset for it and add it the the gui folder
	//TODO Move all assets to AssetPlacementKeys?
	public GameObject marker = null;

	
	//TODO Add some zplane control for the placed assets or something
	public float distance = 500;
	public bool isMarkerActive = false;
	
	public static AssetPlacementPositionSystem instance = null;
	void Awake() {
		if (instance && instance != this) {
			Destroy (gameObject);
		} else {
			DontDestroyOnLoad(gameObject);
			instance = this;
		}
	}
	
	Vector3 FindPlacementPosition () {
		Vector2 fixedPos = new Vector2 (Event.current.mousePosition.x + adjustX, -Event.current.mousePosition.y + Screen.height + adjustY);
		var ray = Camera.current.ScreenPointToRay (fixedPos);
		Vector3 position = ray.GetPoint (distance);
		xPosition = position.x;
		yPosition = position.y;
		selectedPosition = new Vector3 (xPosition, yPosition, distance);
		return position;
	}
	
	bool ShouldMoveMarker () {
		if (!isMarkerActive) {
			if (marker) { marker.SetActive (false); }
			return false;
		} 
		
		if (marker) { 
			marker.SetActive (true);
			return true;
		}
		
		return false;
	}
	
	void MoveDebugMarker (Vector3 position) {
		if (ShouldMoveMarker ()) {
			marker.transform.localPosition = position;
			marker.transform.localScale = Vector3.one;
		}
	}
	
	public void OnDrawGizmos() {
		if (Camera.current) {
			var position = FindPlacementPosition ();
			MoveDebugMarker (position);
		}
	}
}
