using UnityEngine;
using System.Collections;

public class Credits : Touchable
{
	
	public override void OnTouchStart()
	{
		
		guiText.material.color = Color.red;
	}
	
	public override void OnTouchEnd()
	{
		Application.LoadLevel("Credits");
	}
	
	
	public override void OnTouch(){}
	                
}