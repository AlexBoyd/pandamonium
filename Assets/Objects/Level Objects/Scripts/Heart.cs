using UnityEngine;
using System.Collections;

public class Heart : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
	{
		print("hhh");
		if(collision.collider.tag == "Player")
		{
			gameObject.active = false;
		}
	}
}
