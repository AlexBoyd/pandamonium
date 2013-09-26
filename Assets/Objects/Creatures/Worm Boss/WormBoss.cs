using UnityEngine;
using System.Collections;
using System;

public class WormBoss : MonoBehaviour {

    DateTime lastSpit;
	public GameObject blood;
	public AudioSource hitSound;
	int numHits = 0;
	
	void OnCollisionEnter (Collision collision)
	{
        if (collision.collider.tag == "p_box" && collision.collider.rigidbody.velocity.y < -20)
        {
			collision.gameObject.active = false;
//			collision.collider.enabled = false;
			numHits++;
			GameObject head = GameObject.Find("Head");
			Instantiate (blood, head.transform.position, Quaternion.identity);
			hitSound.Play();
            if (numHits == 3)
            {
                gameObject.SetActiveRecursively(false);
            }
        }
	}
}
