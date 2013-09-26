using UnityEngine;
using System.Collections;

public class IntroCamera : MonoBehaviour {
	Vector3 initialPos;
	int zoom = 0;
	
	// Use this for initialization
	void Start () {
		initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			transform.position = initialPos;
			zoom = 0;
		}
		
		if(zoom < 700){
			Vector3 newPos = transform.position;
			newPos.z += 0.03f*Time.deltaTime;
			transform.position = newPos;
			zoom++;
		}
	}
}
