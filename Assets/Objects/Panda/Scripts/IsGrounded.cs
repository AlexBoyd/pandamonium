using UnityEngine;
using System.Collections;

public class IsGrounded : MonoBehaviour {

	public bool grounded;
	
	void OnTriggerStay(Collider other){
		if(other.gameObject.layer == LayerMask.NameToLayer ("Ground")){
			grounded = true;
		}
	}
	void OnTriggerExit(Collider other){
		if(other.gameObject.layer == LayerMask.NameToLayer ("Ground")){
			grounded = false;
		}
	}
}
