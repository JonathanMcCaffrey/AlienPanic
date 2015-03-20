#if UNITY_EDITOR
//This code is generated dynamically. Don't edit here
//Edit at APChoiceSystem.cs line: 195
using UnityEditor; 
using UnityEngine; 

public class APSerializedHotKeys : EditorWindow {
	static void RefreshSelectedKey (KeyCode hotkeyCode) {
		if (hotkeyCode != KeyCode.None) {
			int index = 0;
			foreach (var assetData in APChoiceSystem.instance.assetList) {
				if(APChoiceSystem.instance.getSelectedTab().name == assetData.tab) {
					if (assetData.keyCode == hotkeyCode) {
						EditorPrefs.SetInt (APGlobals.SelectedAssetNumber, index);
						EditorPrefs.SetInt (APGlobals.SelectedKey, (int)hotkeyCode);

						if(APChoiceSystem.instance) {
							APChoiceSystem.instance.OnDrawGizmos();
						}

						return;
					}
				}
			index++;
			}
		}
	}

	[MenuItem( APGlobals.CommandPath + "Hot Keys/None &_None")]
	public static void SelectItemNone() {
		EditorPrefs.SetInt (APGlobals.SelectedKey, (int)KeyCode.None); 
		EditorPrefs.SetInt (APGlobals.SelectedAssetNumber, APGlobals.HotKeySelectionEnabled);
		RefreshSelectedKey(KeyCode.None);
	}


	[MenuItem( APGlobals.CommandPath + "Hot Keys/Alpha1 &_Alpha1")]
	public static void SelectItemAlpha1() {
		EditorPrefs.SetInt (APGlobals.SelectedKey, (int)KeyCode.Alpha1); 
		EditorPrefs.SetInt (APGlobals.SelectedAssetNumber, APGlobals.HotKeySelectionEnabled);
		RefreshSelectedKey(KeyCode.Alpha1);
	}


	[MenuItem( APGlobals.CommandPath + "Hot Keys/Alpha2 &_Alpha2")]
	public static void SelectItemAlpha2() {
		EditorPrefs.SetInt (APGlobals.SelectedKey, (int)KeyCode.Alpha2); 
		EditorPrefs.SetInt (APGlobals.SelectedAssetNumber, APGlobals.HotKeySelectionEnabled);
		RefreshSelectedKey(KeyCode.Alpha2);
	}


	[MenuItem( APGlobals.CommandPath + "Hot Keys/Alpha3 &_Alpha3")]
	public static void SelectItemAlpha3() {
		EditorPrefs.SetInt (APGlobals.SelectedKey, (int)KeyCode.Alpha3); 
		EditorPrefs.SetInt (APGlobals.SelectedAssetNumber, APGlobals.HotKeySelectionEnabled);
		RefreshSelectedKey(KeyCode.Alpha3);
	}


	[MenuItem( APGlobals.CommandPath + "Hot Keys/Alpha4 &_Alpha4")]
	public static void SelectItemAlpha4() {
		EditorPrefs.SetInt (APGlobals.SelectedKey, (int)KeyCode.Alpha4); 
		EditorPrefs.SetInt (APGlobals.SelectedAssetNumber, APGlobals.HotKeySelectionEnabled);
		RefreshSelectedKey(KeyCode.Alpha4);
	}


	[MenuItem( APGlobals.CommandPath + "Hot Keys/Alpha5 &_Alpha5")]
	public static void SelectItemAlpha5() {
		EditorPrefs.SetInt (APGlobals.SelectedKey, (int)KeyCode.Alpha5); 
		EditorPrefs.SetInt (APGlobals.SelectedAssetNumber, APGlobals.HotKeySelectionEnabled);
		RefreshSelectedKey(KeyCode.Alpha5);
	}


	[MenuItem( APGlobals.CommandPath + "Hot Keys/Alpha6 &_Alpha6")]
	public static void SelectItemAlpha6() {
		EditorPrefs.SetInt (APGlobals.SelectedKey, (int)KeyCode.Alpha6); 
		EditorPrefs.SetInt (APGlobals.SelectedAssetNumber, APGlobals.HotKeySelectionEnabled);
		RefreshSelectedKey(KeyCode.Alpha6);
	}

} 
#endif