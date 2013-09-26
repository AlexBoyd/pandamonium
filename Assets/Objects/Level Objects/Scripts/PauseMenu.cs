using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	//public GUISkin mySkin;
	public Texture2D backgroundTexture;
	
	private Rect windowRect;
	private bool paused = false;
	private bool waited = true;
	private GUITexture pause;
	private static float width = Screen.width;
	private static float height = Screen.height;
	private static float ratiowth = width/height; 
	
	private void Start() {
		float width = Screen.width;
	 	float height = Screen.height;
	 	float ratiowth = width/height; 
		windowRect = new Rect(Screen.width/4, Screen.height/8, Screen.width/2, 3*Screen.height/4);	
		GameObject p = GameObject.Find("Pause");
		pause = (GUITexture)p.GetComponent (typeof(GUITexture));
	}
	
	private void waiting(){
		waited = true;
	}
	
	private void Update() {
		
		if(waited){
			foreach (Touch touch in Input.touches){
				if(pause.HitTest(touch.position)){
					if(paused) {
						dismissPausedWindow();
					}else {
						showPausedWindow();
					}
					waited = false;
					Invoke("waiting", 0.3f);
				}
			}
			
			if(Input.GetKey(KeyCode.P)) {
				if(paused){
					dismissPausedWindow();
				} else {
					showPausedWindow();
				}
				waited = false;
				Invoke("waiting", 0.3f);
			}
		}
		
		if(paused){
			Time.timeScale = 0;
		}
		else {
			Time.timeScale = 1;
		}
		
	}
	
	private void OnGUI(){
		//GUI.skin = mySkin;
		if(paused) {
			windowRect = GUI.Window(0,windowRect, windowFunc, " ");	
			GUI.DrawTexture(windowRect, backgroundTexture);
		}
	}
	
	private void showPausedWindow() {
		paused = true;
		print("On gui running");
		//GUI.Box(windowRect, backgroundTexture);
		
	}
	
	private void dismissPausedWindow() {
		paused = false;
	}
		
	private void windowFunc(int id){
		GUILayout.Space(30);
		if(GUILayout.Button("Resume")){
			paused = false;
		}
		GUILayout.Space(5);
		//GUILayout.BeginHorizontal();
		if(GUILayout.Button("Options")){
		}
		GUILayout.Space(5);
		if(GUILayout.Button("Quit")){
			dismissPausedWindow();
			LevelManager.LoadLevel("3D Menu");
			return;
		}
		//GUILayout.EndHorizontal();
	}
}
