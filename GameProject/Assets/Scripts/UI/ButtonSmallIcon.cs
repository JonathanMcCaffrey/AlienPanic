using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonSmallIcon : MonoBehaviour {
	public bool disableModifiedControls = false;
	
	public GameObject icon = null;
	public GameObject background = null;
	public GameObject button = null;
	
	public Sprite iconSprite = null;
	public Color backgroundColor = new Color(204,104,0, 216);
	public List<EventDelegate> onButtonClick = new List<EventDelegate>();
	
	
	UI2DSprite getIcon() {
		return  icon.GetComponent<UI2DSprite> ();
	}
	
	UISprite getBackground() {
		return  background.GetComponent<UISprite> ();
	}
	
	UIButton getButton() {
		return  button.GetComponent<UIButton> ();
	}
	
	public void onClick() {
		Debug.Log ("Button OnClick Not Set");
	}
	
	void RefreshButton () {
		if (iconSprite) {
			if (getIcon ().name != iconSprite.name) {
				getIcon ().sprite2D = iconSprite;
			}
		}
		if (getBackground ().color != backgroundColor) {
			getBackground ().color = backgroundColor;
		}
		if (getButton ().onClick != onButtonClick) {
			getButton ().onClick = onButtonClick;
		}
	}
	
	public void OnDrawGizmos() {
		if (disableModifiedControls) {
			return;
		}
		
		if (!icon || !background || !button) {
			return;
		}
		
		RefreshButton ();
	}
}
