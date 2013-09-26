using UnityEngine;
using System.Collections;

public class SkipButton : Touchable {
	public GameObject loaderPrefab;
	public string levelToLoad;
	public string checkpointToLoad;
	
	private GUITexture skipButton;
	
	// Use this for initialization
	void Start () {
		GameObject skip = GameObject.Find ("skip");
		skipButton = (GUITexture)skip.GetComponent (typeof(GUITexture));
	}
	
	void Update() {
		if(Input.GetKeyUp(KeyCode.S)) {
			if(checkpointToLoad == "end"){
				Application.LoadLevel(levelToLoad);
			}
			else{
				SaveManager.SaveRawCheckpointData(levelToLoad, checkpointToLoad);
				GameObject loader = Instantiate(loaderPrefab) as GameObject;	
				loader.GetComponent<Loading>().Load();	
			}
		}
		foreach (Touch touch in Input.touches) {
			if(checkpointToLoad == "end"){
				Application.LoadLevel(levelToLoad);
			}
			else{
				SaveManager.SaveRawCheckpointData(levelToLoad, checkpointToLoad);
				GameObject loader = Instantiate(loaderPrefab) as GameObject;	
				loader.GetComponent<Loading>().Load();	
			}
		}
	}

	
	// Update is called once per frame
	public override void OnTouchStart () {
		
	}
	
	public override void OnTouch(){
		
		
	}
	public override void OnTouchEnd(){
	
	}
}
