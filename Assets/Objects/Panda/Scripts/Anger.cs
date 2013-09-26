using UnityEngine;
using System.Collections;
using System;

public class Anger : MonoBehaviour {

	private bool isActiveAngry = false;
	private bool isPassiveAngry = false;
	public bool isAngry = false;
	private GameObject lastCube;
	public GameObject hint;
	public AudioSource sound;
	
	public GameObject angerMarker;
	private GameObject myMarker;
	
	//Box movement stuff
	private GameObject boxHeld;
	private bool isHoldingBox = false;
	private float HOLD_CUBE_DISTANCE_THRESHOLD = 2.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(isActiveAngry) {
			if(myMarker != null) {
				myMarker.transform.position = gameObject.transform.position;	
			}
		}
	}
	
	void FixedUpdate() {
		doBoxMove();	
	}
	
	
	void OnTriggerEnter(Collider other){
		if (other.tag == "Anger" && SaveManager.IsAngerUnlocked()){
			addAnger();
			Destroy(other.gameObject);
			sound.Play();
		}
		else if (other.tag == "Anger" && !SaveManager.IsAngerUnlocked()){
			hint.GetComponent<GUITexture>().enabled = true;
			StartCoroutine("disableTimer");
		}
	}
	
	IEnumerator disableTimer()
	{
		yield return new WaitForSeconds(1.5f);
		hint.GetComponent<GUITexture>().enabled = false;
	}

	
	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "p_box") {
			lastCube = collision.gameObject;
		}
	}
	
	private void addAnger() {
		Anger anger = gameObject.GetComponent<Anger> ();
		if(!anger.isAngry) {
			isActiveAngry = true;
			isPassiveAngry = true;
			isAngry = true;
			rigidbody.mass = 1;
			GameObject halo = GameObject.Find("AngerHalo");
	    	(halo.GetComponent("Halo") as Behaviour).enabled = true;
		
			//Remove the other abilities
			Happy happy = gameObject.GetComponent<Happy> ();
			if(happy.isHappy) {
				happy.removeHappy();
			}
			Courage courage = gameObject.GetComponent<Courage>();
			if(courage.isCourage) {
				courage.removeCourage();
			}
		}
	}
	
	public void removeAnger() {
		isActiveAngry = false;
		isPassiveAngry = false;
		isAngry = false;
		rigidbody.mass = 0.0001f;
		GameObject halo = GameObject.Find("AngerHalo");
	    (halo.GetComponent("Halo") as Behaviour).enabled = false;
//		if(myMarker != null) {
//			Destroy(myMarker);
//		}
	}
	
	
	
	public bool isPandaActiveAngry() {
		return isActiveAngry;	
	}
	
	public bool isPandaPassiveAngry() {
		return isPassiveAngry;	
	}
	
	//Do this when the O button is hit
	public void doAnger() {
		if (isHoldingBox) {
			dropBox();
		} else {
			if (isBoxWithinRange (lastCube)) {
				pickupBox (lastCube);
			}
		}
	}
	
	
	private void pickupBox (GameObject box)
	{
		if(box.tag == "p_box") {
			isHoldingBox = true;
			boxHeld = box;
			boxHeld.rigidbody.useGravity = false;
		}
//		Vector3 boxPosition = box.rigidbody.position;
//		box.rigidbody.position = new Vector3(boxPosition.x, boxPosition.y+1, boxPosition.z);
//		box.rigidbody.constraints = RigidbodyConstraints.FreezePositionY|box.rigidbody.constraints;
	}
	
	
	
	private bool isBoxWithinRange (GameObject cube)
	{
		if (cube != null) {
			float distance = Vector3.Distance (transform.position, cube.transform.position);
			if (distance < HOLD_CUBE_DISTANCE_THRESHOLD) {
				return true;
			} 
		} 
		return false;
	}
	
	


	void doBoxMove ()
	{
		if (isActiveAngry) {
			if (isHoldingBox) {
				Vector3 pandaPosition = rigidbody.position;
				Move m = gameObject.GetComponent<Move>();
				int lastPandaDirectionX = m.getLastPandaDirectionX();
				boxHeld.rigidbody.MovePosition(new Vector3 (pandaPosition.x + (2 * lastPandaDirectionX), pandaPosition.y + boxHeld.transform.localScale.y / 2, pandaPosition.z));
			}
		} else {
			if (boxHeld != null) {
				dropBox ();
			}
		}
	}

	private void dropBox ()
	{
		isHoldingBox = false;
		boxHeld.rigidbody.useGravity = true;
		boxHeld = null;
	}
	
	//:( :( :( :( :(
//	private void throwBox (int power)
//	{
//		if (isHoldingBox && boxHeld != null) {
//			boxHeld.rigidbody.useGravity = true;
//			
//			//Deal with the power var
//			print("MS: " + power + " Power: " + power/40);
//			power = Mathf.Min(power/40, 30);
//			
//			
//			Move m = gameObject.GetComponent<Move>();
//			int lastPandaDirectionX = m.getLastPandaDirectionX();
//			boxHeld.rigidbody.velocity = new Vector3 (power * lastPandaDirectionX, (int)(power*1.1), 0);
//			dropBox ();
//		}
//	}


}
