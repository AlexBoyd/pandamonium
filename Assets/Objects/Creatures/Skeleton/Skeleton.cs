using UnityEngine;
using System.Collections;

public class Skeleton : Triggerable {
	
	private bool spawneded = false;
	private bool dieing = false;
	private Vector3 spawnPosition;
	private GameObject Panda;
	
	//these are made public for the sakes of tweaks at runtime via inspector during prototyping, but should be finalized as constants
	public int ACCERLATION_FROM_DISTANCE = 750;
	public  int BASE_ACCLERATION = 100;
	public  float ACCLERATION_DECAY_CONST1 = 10;
	public float ACCLERATION_DECAY_CONST2 = 5;
	public float FADE_SPEED = 0.1f;
	public float MAX_VELOCITY;
	// Use this for initialization
	void Start () {
		spawnPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(spawneded && !dieing)
		{
			//calculate difference between the center of the two objects,
			float positionDifference =  (Panda.transform.position.x + Panda.transform.localScale.x/2) - (transform.position.x + transform.localScale.x/2);
			//Do some tricky math to get the acceleration / chase speed to feel about right. 
			rigidbody.AddForce((ACCERLATION_FROM_DISTANCE / (10 + ACCLERATION_DECAY_CONST1 * Mathf.Log(Mathf.Abs(positionDifference)+ACCLERATION_DECAY_CONST2)) + BASE_ACCLERATION) * Mathf.Sign(positionDifference),0,0);
			if (Mathf.Abs(positionDifference) > 10)
			{
				print("dieing!");
				dieing = true;
				rigidbody.velocity = Vector3.zero;
				StartCoroutine(skeletonDeathCoroutine());
			}
							
		}
		if(rigidbody.velocity.x > MAX_VELOCITY)
		{
			rigidbody.velocity = new Vector3(MAX_VELOCITY,rigidbody.velocity.y, rigidbody.velocity.z);
		}
	}
	
	private IEnumerator skeletonDeathCoroutine()
	{
		while(!(renderer.material.color.a > 0))
		{
			print("fading");
			renderer.material.SetAlpha(renderer.material.color.a - FADE_SPEED);
			yield return new WaitForFixedUpdate();
		}
		rigidbody.MovePosition(spawnPosition);
		spawneded = false;
		dieing = false;
	}
	
	private IEnumerator skeletonSpawnCoroutine()
	{
		while((renderer.material.color.a < 1))
		{
			print("spawning");
			renderer.material.SetAlpha(renderer.material.color.a + FADE_SPEED);
			yield return new WaitForFixedUpdate();
		}
		spawneded = true;
	}
	
	public override void Trigger ()
	{
		Panda = GameObject.FindGameObjectWithTag("Panda").transform.FindChild("Panda").gameObject;
		StartCoroutine(skeletonSpawnCoroutine());
	}
}
