using UnityEngine;
using System.Collections;

public class HeartPieceGUI : MonoBehaviour {
	public Texture heartz0;
	public Texture heartz1;
	public Texture heartz2;
	public Texture heartz3;
	
	public GUITexture heart;
	
	// Use this for initialization
	void Start () {
		if(SaveManager.IsCourageUnlocked()){
			heart.texture = heartz3;
		}
		
		else if(SaveManager.IsHappinessUnlocked()){
			heart.texture = heartz2;
		}
		
		else if(SaveManager.IsAngerUnlocked()) {
			heart.texture = heartz1;
		}
		
		else{
			heart.texture = heartz0;	
		}
	}
	
	void OnCollissionEnter(Collision collision) {
		if(collider.collider.tag == "Heart") {
			if(SaveManager.IsCourageUnlocked()){
			heart.texture = heartz3;
			}
		
			else if(SaveManager.IsHappinessUnlocked()){
			heart.texture = heartz2;
			}
		
			else if(SaveManager.IsAngerUnlocked()) {
			heart.texture = heartz1;
			}
		
			else{
			heart.texture = heartz0;	
			}
		}
	}
}
