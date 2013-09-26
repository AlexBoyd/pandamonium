using UnityEngine;
using System.Collections;

public class TutorialTip : Triggerable {
	bool isShowing = false;
	public float up;
	public float horiz;
	
	public override void Trigger(){
		if(!isShowing){
			Vector3 newPos = transform.position;
			newPos.z -= 3f;
			newPos.y += up;
			newPos.x += horiz;
			transform.position = newPos;
			
			isShowing = true;
		}
	}
}
