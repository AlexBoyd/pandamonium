using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Death : MonoBehaviour
{
	
	public GameObject blood;
	public AudioSource deathSound;
	private int animStart;
	public bool pCollision = false;
	public bool dead = false;
	public GameObject loaderPrefab;
	public GameObject PandaRagdoll;
	public GameObject PandaModel;
	private float deadFor = 0f;
	private float DEAD_TIME = 0.8f;
	public static List<string> deathObjects = new List<string> ()	
	{
		"Spike",
		"Badger",
		"Spider",
        "Honey_Badger_Boss",
        "Lava"
	};
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		//seriously unity?
		if (dead) {
			deadFor += Time.deltaTime;
			print (deadFor);
			if (deadFor > DEAD_TIME) {
				dead = false;
				GameObject loader = Instantiate (loaderPrefab) as GameObject;
				loader.GetComponent<Loading> ().Load ();
			}
		}
		
	}
	
	void OnParticleCollision (GameObject other)
	{
		killPanda ();
	}
	
	void OnCollisionEnter (Collision collision)
	{
		if (deathObjects.Contains (collision.collider.tag)) {
			killPanda ();
		}
	}
	
	private void killPanda ()
	{
		if(!dead) {
			GetComponent<Move>().enabled = false;
			GetComponent<CameraFollow>().enabled = false;
			GameObject halo = GameObject.Find("CourageHalo");
	    	(halo.GetComponent("Halo") as Behaviour).enabled = false;
			halo = GameObject.Find("AngerHalo");
			(halo.GetComponent("Halo") as Behaviour).enabled = false;
			halo = GameObject.Find("HappyHalo");
			(halo.GetComponent("Halo") as Behaviour).enabled = false;
			rigidbody.mass = 10;
			Instantiate (blood, rigidbody.position, Quaternion.identity);
			deathSound.Play();
			Instantiate (PandaRagdoll, rigidbody.position, Quaternion.identity);
			PandaRagdoll.transform.Rotate (new Vector3 (15, 15, 15));	
			PandaModel.SetActiveRecursively (false);
			dead = true;
		}
	}
		

}
