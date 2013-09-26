	using UnityEngine;
using System.Collections;

public class ProximityTrigger : MonoBehaviour {
	
	public Triggerable triggerable;
	
	void OnTriggerEnter(Collider other)
	{
		if(triggerable != null && other.tag == "Player")
		{
			triggerable.Trigger();
		}
	}
}
