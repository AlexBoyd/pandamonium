using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Camera.mainCamera.transform.position = transform.position + new Vector3(0,3,-5);	
	}
}
