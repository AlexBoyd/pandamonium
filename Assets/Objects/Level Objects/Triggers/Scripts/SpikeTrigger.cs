using UnityEngine;
using System.Collections;

public class SpikeTrigger : MonoBehaviour {

	public Triggerable triggerable;
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Cube")
		{
			triggerable.Trigger();
		}
	}
}
