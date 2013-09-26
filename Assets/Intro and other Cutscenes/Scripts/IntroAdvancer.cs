using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntroAdvancer : Touchable {
	
	public GameObject loaderPrefab;
	public string levelToLoad;
	public string checkpointToLoad;
	public Texture[] cell1;
	public Texture[] cell2;
	public Texture[] cell3;
	public Texture[] cell4;
	public Texture[] cell5;
	public Texture[] cell6;
	public Texture[] cell7;
	public Texture[] cell8;
	public Texture[] cell9;
	public Texture[] cell10;
	public Texture[] cell11;
	public Texture[] cell12;

	private int currentFrame = 0;
	private GUITexture skipButton;
	Texture[][] backgrounds = new Texture[12][];
	private Touch touch;
	
	void Start(){
		backgrounds[0] = cell1; 
		backgrounds[1] = cell2;
		backgrounds[2] = cell3;
		backgrounds[3] = cell4;
		backgrounds[4] = cell5;
		backgrounds[5] = cell6;
		backgrounds[6] = cell7;
		backgrounds[7] = cell8;
		backgrounds[8] = cell9;
		backgrounds[9] = cell10;
		backgrounds[10] = cell11;
		backgrounds[11] = cell12;
		
		nextScene();
		
	}
	
	void Update() {
		foreach (Touch touch in Input.touches) 
		{
			if(touch.phase == TouchPhase.Ended) {
				nextScene();
			}
		}
	}
	
	void OnMouseButtonUp() {
		nextScene();
	}
	
	public override void OnTouchStart()
	{
		nextScene();
	}
		                   
	public override void OnTouch(){}
	public override void OnTouchEnd(){}
	
	
	private void nextScene() {
		print(currentFrame);
		if(currentFrame < backgrounds.Length -1 && backgrounds[currentFrame][0] != null){
			StopCoroutine("PlayFrames");
			StartCoroutine("PlayFrames", backgrounds[currentFrame]);
			currentFrame++;
		}
		
		if(currentFrame > backgrounds.Length || backgrounds[currentFrame].Length == 0)
		{
			if( checkpointToLoad != "end"){
				SaveManager.SaveRawCheckpointData(levelToLoad, checkpointToLoad);
				GameObject loader = Instantiate(loaderPrefab) as GameObject;	
				loader.GetComponent<Loading>().Load();
			}
			
			else{
				Application.LoadLevel(levelToLoad);	
			}
		}
	}
	
		IEnumerator PlayFrames(Texture[] cell){
		int i;
		while(true){
			for(i = 0 ; i<cell.Length ; i++){
				yield return new WaitForSeconds(.5f);
				guiTexture.texture = (cell[i]);
			}
			i = 0;
		}
	}
}
