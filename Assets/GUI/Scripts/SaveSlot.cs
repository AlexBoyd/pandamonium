using UnityEngine;
using System.Collections;

public class SaveSlot : Touchable{

	public bool createNewSave = false;
	public int saveSlot;
	public GameObject loaderPrefab;
	
	public override void OnTouchStart()
	{
		guiText.material.color = Color.red;
	}
	
	public override void OnTouchEnd()
	{
		SaveManager.switchSaveSlot(saveSlot);
		SaveManager.LoadSaveFile();	
		if(createNewSave)
		{
			SaveManager.ClearSaveSlot();
			Application.LoadLevel("IntroScene");
		}
		else
		{
			GameObject loader = Instantiate(loaderPrefab) as GameObject;
			loader.GetComponent<Loading>().Load();
		}
		
	}
	
	public override void OnTouch(){}
	
}
