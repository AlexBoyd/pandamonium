using UnityEngine;
using System.Collections;

public class PressurePad : MonoBehaviour {

	public Triggerable triggerable;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other)
	{
		{
			triggerable.Trigger();	
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		{
			triggerable.UnTrigger();	
		}
	}
		
}
