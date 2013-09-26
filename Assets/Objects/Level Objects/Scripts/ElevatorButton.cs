using UnityEngine;
using System.Collections;

public class ElevatorButton : Touchable {

	public string levelName;
	public GameObject loaderPrefab;
	
	private Touch touch;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void Update(){

	}
	
	// Update is called once per frame
	public override void OnTouchStart() {
		GameObject loader = Instantiate(loaderPrefab) as GameObject;
		loader.GetComponent<Loading>().LoadElevator(levelName);
	}
	
	public override void OnTouch(){}
	public override void OnTouchEnd(){}
	
}
