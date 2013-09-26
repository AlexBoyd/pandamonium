using UnityEngine;
using System.Collections;

public class LevelRoot : MonoBehaviour {
	
	public bool angerLevel = false;
	public bool happyLevel = false;
	public bool courageLevel = false;
	public bool bossLevel = false;
	
	// Use this for initialization
	void Start () {
		LevelManager.loadedLevels.Add(name);
	}	
	
	void OnDestroy() {
		LevelManager.loadedLevels.Remove(name);	
	}
	
	public bool isDark() {
		return courageLevel;
	}
	
	public bool isBoss() {
		return bossLevel;
	}
	
	public bool isHappy(){
		return happyLevel;
	}
	
	public bool isAngry(){
		return angerLevel;	
	}
	
}