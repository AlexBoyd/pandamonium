using UnityEngine;
using System.Collections;

public class Back : MonoBehaviour
{

	void Start() {
		
	}

	
	void Update() {
		if(Input.GetKeyUp(KeyCode.Escape)) {
			Application.LoadLevel("3D Menu");
			return;
		}	
	}
	
	                
}
