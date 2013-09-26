using UnityEngine;
using System.Collections;

public abstract class Touchable: MonoBehaviour{
		
	abstract public void OnTouchStart();
	
	abstract public void OnTouch();
	
	abstract public void OnTouchEnd();
	
	public void OnMouseDown()
	{
		OnTouchStart();	
	}
	
	public void OnMouseUp()
	{
		OnTouchEnd();	
	}
}
