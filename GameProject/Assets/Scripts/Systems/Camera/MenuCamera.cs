using UnityEngine;
using System.Collections;

public class MenuCamera : MonoBehaviour {
	Camera getCamera() {
		return gameObject.GetComponent<Camera> ();
	}
	
	
	
	
	public void OnDrawGizmos() {
	//	Rect cameraRect = new Rect (0, 0, Screen.width, Screen.height);
	//	getCamera ().rect = cameraRect;
		
		
	//	GetComponent<UIWidget> ().width = (int)getCamera ().rect.width;
	//	GetComponent<UIWidget> ().height = (int)getCamera ().rect.height;
		
	}
	
}
