using UnityEngine;
using System.Collections;

public class MenuPanel : MonoBehaviour {
	
	public void OnDrawGizmos() {
		Rect screenRect = new Rect (0, 0, Screen.width, Screen.height);
		
		gameObject.GetComponent<UIPanel> ().SetRect (screenRect.x, screenRect.y, screenRect.width, screenRect.height);
	}
}
