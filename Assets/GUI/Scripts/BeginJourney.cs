using UnityEngine;
using System.Collections;

public class BeginJourney : Touchable {
	
	public GameObject mainMenu;
	public GameObject saveMenu;
	
	public override void OnTouchStart()
	{
		guiText.material.color = Color.red;
	}
	
	public override void OnTouchEnd()
	{
		foreach(GUIText menuItem in mainMenu.GetComponentsInChildren<GUIText>())
		{
			menuItem.enabled = false;
		}
		foreach(GUIText saveSlot in saveMenu.GetComponentsInChildren<GUIText>())
		{
			saveSlot.enabled = true;
		}
		foreach(SaveSlot saveSlot in saveMenu.GetComponentsInChildren<SaveSlot>())
		{
			saveSlot.createNewSave = true;
		}
	}
	
	public override void OnTouch(){}
}
