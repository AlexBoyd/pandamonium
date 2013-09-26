using UnityEngine;
using System.Collections;

public class LavaMonster : Triggerable {
	
	private bool spawneded = false;
	private bool dieing = false;
	private Vector3 spawnPosition;
	private GameObject Panda;
	public Material stone;
	
	//these are made public for the sakes of tweaks at runtime via inspector during prototyping, but should be finalized as constants
	public int ACCERLATION_FROM_DISTANCE = 750;
	public  int BASE_ACCLERATION = 100;
	public  float ACCLERATION_DECAY_CONST1 = 5;
	public float ACCLERATION_DECAY_CONST2 = 2;
	public float FADE_SPEED = 0.1f;
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
			rigidbody.AddForce((ACCERLATION_FROM_DISTANCE / (1 + ACCLERATION_DECAY_CONST1 * Mathf.Log(Mathf.Abs(positionDifference)+ACCLERATION_DECAY_CONST2)) + BASE_ACCLERATION) * Mathf.Sign(positionDifference),0,0);
			if (Mathf.Abs(positionDifference) > 5)
			{
				print("dieing!");
				dieing = true;
				rigidbody.velocity = Vector3.zero;
				lavaMonsterDeath();
			}
							
		}
	}
	
	private void lavaMonsterDeath()
	{
		renderer.material = stone;
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
