using UnityEngine;
using System.Collections;

public class CubeBehavior : MonoBehaviour {
	
	private int DESTROY_BOX_FORCE = 10000;
	private GameObject lastCubeHit;
	public GameObject destroyBoxParticle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision collision) {
		if(collision.impactForceSum.sqrMagnitude > DESTROY_BOX_FORCE) {
			Instantiate(destroyBoxParticle, rigidbody.position, Quaternion.identity);
			Destroy(gameObject);
			
		}
		
	}
}
