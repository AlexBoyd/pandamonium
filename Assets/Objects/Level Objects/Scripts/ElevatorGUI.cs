using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElevatorGUI : Triggerable {
	const int NUM_ROOMS = 4;
	
	public GUITexture[] buttons = new GUITexture[NUM_ROOMS]; 
	public GUITexture[] instances = new GUITexture[NUM_ROOMS]; 
	
	//get elevators visited so far
	private List<string> levels;
	bool showing = false;
	
	// Use this for initialization
	void Start () {
	}
	
	//if panda is on checkpoint- show elecator buttons
	public override void Trigger(){
		levels = SaveManager.GetUnlockedLevels();
		print(levels.Count);
		
		//place buttons on screen according to screen size
		float x = .17f;
		
		if(!showing){
			for(int i = 0; i<levels.Count; i++){
				Vector3 newPos = new Vector3(x+x*i,.5f,0);
				buttons[i].transform.position = newPos;
				instances[i] = (GUITexture)Instantiate(buttons[i]);
			}
				
			showing = true;
		}
	}
	
	public override void UnTrigger()
	{	
		if(showing){
			for(int i=0; i<levels.Count; i++){
				Destroy(instances[i].gameObject);
			}
			showing = false;
		}
	}
}
