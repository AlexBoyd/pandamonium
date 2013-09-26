using UnityEngine;
using System.Collections;
using System;

public class Happy : MonoBehaviour
{
	public AudioSource sound;
	private bool isActiveHappy = false;
	private bool isPassiveHappy = false;
	public bool isHappy = false;
	public GameObject happyCloud;
	public GameObject happyMarker;
	private GameObject myCloud;
	private GameObject myMarker;
//	private int HAPPY_TIME_LIMIT = 60;
//	private float DEFAULT_MAGNITUDE = 20f;
//	private float speed = 200.0F;
	private bool onCloud = false;
	public GameObject hint;
	private bool cloudCalled = false;
	private bool cloudFade = false;

	public void Float()
	{
		// Fade out cloud if pressing o while on cloud.
//		if(onCloud)
//		{
//			cloudFade = true;
//		}
		if (!onCloud && gameObject.GetComponent<Move>().isGrounded()) {
		onCloud = true;
		gameObject.rigidbody.useGravity = false;
		
		gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
		
		gameObject.rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
		createCloud();
		}
		else {
		onCloud = true;
		gameObject.rigidbody.useGravity = false;
		
		gameObject.rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
		createCloud();
		}
	}
	
	public void createCloud ()
	{
		
		//Create cloud or set it to active
		if (myCloud == null) {
			myCloud = Instantiate (happyCloud) as GameObject;
		}
		else
		{
			myCloud.active = true;
		}
		
		cloudCalled = true;
		StartCoroutine("cloudTimer");

	}
	
		IEnumerator cloudTimer()
	{
		yield return new WaitForSeconds(2f);
		cloudCalled = false;
		cloudFade = true;
	}
	
	
	// Update is called once per frame
	void Update ()
	{
		if(cloudCalled && (myCloud.renderer.material.color.a < 1))
		{
			myCloud.renderer.material.SetAlpha(myCloud.renderer.material.color.a + 0.05f);
		}
		if (myCloud != null && myCloud.active) 
		{
			myCloud.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
		}
		
		if(cloudFade)
		{
			myCloud.renderer.material.SetAlpha(myCloud.renderer.material.color.a - 0.05f);
			if(!(myCloud.renderer.material.color.a > 0))
			{
				myCloud.active = false;
				gameObject.rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
				gameObject.rigidbody.useGravity = true;
				onCloud = false;
				cloudFade = false;
			}
		}

	}
		
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Happy" && SaveManager.IsHappinessUnlocked()) {
			addHappy ();
			Destroy (other.gameObject);
			sound.Play();
		}
		else if (other.tag == "Happy" && !SaveManager.IsHappinessUnlocked()){
			hint.GetComponent<GUITexture>().enabled = true;
			StartCoroutine("disableTimer");
		}
	}
	
	IEnumerator disableTimer()
	{
		yield return new WaitForSeconds(1.5f);
		hint.GetComponent<GUITexture>().enabled = false;
	}
	
	public bool isPandaActiveHappy ()
	{
		return isActiveHappy;
	}
	
	public bool isPandaPassiveHappy ()
	{
		return isPassiveHappy;	
	}
	
	private void addHappy ()
	{
		Happy happy = gameObject.GetComponent<Happy> ();
		if(!happy.isHappy) {
			isActiveHappy = true;
			isPassiveHappy = true;
			isHappy = true;
		
			GameObject halo = GameObject.Find ("HappyHalo");
			(halo.GetComponent ("Halo") as Behaviour).enabled = true;
			
			//Remove the other abilities
			Anger anger = gameObject.GetComponent<Anger> ();
			if (anger.isAngry) {
				anger.removeAnger ();
			}
			Courage courage = gameObject.GetComponent<Courage> ();
			if (courage.isCourage) {
				courage.removeCourage ();
			}
		}		
	}
	
	public void removeHappy ()
	{
		isActiveHappy = false;
		isPassiveHappy = false;
		isHappy = false;
 
		GameObject halo = GameObject.Find ("HappyHalo");
		(halo.GetComponent ("Halo") as Behaviour).enabled = false;
	}
	

	
}
