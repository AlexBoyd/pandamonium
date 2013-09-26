using UnityEngine;
using System.Collections;

public class CloudMovement : MonoBehaviour {
	
	private static float INITIAL_DELTA = .1f;
	private Vector3 totalMovement;
	private bool isCloudMoving = false;
	private float dx = INITIAL_DELTA;
	private float dy = INITIAL_DELTA;
	private GameObject trackThePanda;
	private float CLOUD_TIME = 9;
	private float aliveFor = 0;
	private bool startFade = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(isCloudMoving) {
			
			if(!isWithin()){
				Vector3 delta = new Vector3(dx, dy, 0);
				totalMovement += delta;
				transform.Translate(delta);
			} else {
				gameObject.collider.isTrigger = false;
				isCloudMoving = false;
			}
		}
		aliveFor += Time.deltaTime;
		if(aliveFor > CLOUD_TIME) {
			startFade = true;
		}
		if(startFade) {
			renderer.material.SetAlpha(renderer.material.color.a - .01F);
			if(!(renderer.material.color.a > 0))
			{
				gameObject.active = false;
			}	
		}
	}
	
	public void startAnimation(GameObject panda) {
		trackThePanda = panda;
		
		//If panda is jumping
		Move m = trackThePanda.GetComponent<Move> ();
		
		aliveFor = 0;
		gameObject.active = true;
		startFade = false;
		Vector3 spawnPosition = (panda.transform.position - (new Vector3 (-2, -1, 0)));
		if (!m.isGrounded ()) {
			spawnPosition = (panda.transform.position - (new Vector3 (0, 2, 0)));	
		}
		Vector3 distanceDiff = spawnPosition - transform.position;
		dx = INITIAL_DELTA*distanceDiff.x;
		dy = INITIAL_DELTA*distanceDiff.y;
		isCloudMoving = true;
		gameObject.collider.isTrigger = true;

		
	}
	
	
	private bool isWithin() {
		Vector3 diff = transform.position - trackThePanda.transform.position;
		return (diff.sqrMagnitude < 5);
	}
	
	
	//Movement for the cloud
	public bool moveCloudUp(float magnitude) {
//		Vector3 up = transform.TransformDirection(Vector3.up);
//		RaycastHit hit;
//		RaycastHit hit2;
//		
//		Vector3 v0 = new Vector3(transform.position.x+transform.localScale.x/2, transform.position.y, transform.position.z);
//		Vector3 v1 = new Vector3(transform.position.x-transform.localScale.x/2, transform.position.y, transform.position.z);
//		bool isHit = Physics.Raycast(v0, up, out hit);
//		bool isHit2 = Physics.Raycast(v1, up, out hit2);
//		if((isHit && (hit.collider.tag != "Ground" || hit.distance > 2)) ||
//			isHit2  && (hit2.collider.tag != "Ground" || hit.distance > 2)) {
//			gameObject.transform.Translate(new Vector3(0, magnitude, 0)* Time.deltaTime );
//			return true;
//		} else {
//			return false;	
//		}
		gameObject.transform.Translate(new Vector3(0, magnitude, 0)* Time.deltaTime );
		return true;
	}
	
	public bool moveCloudDown(float magnitude) {
//		Vector3 down = transform.TransformDirection(Vector3.down);
//		RaycastHit hit;
//		bool isHit = Physics.Raycast(transform.position, down, out hit);
//		if(isHit && hit.collider.tag != "Ground" || hit.distance > .5) {
//			gameObject.transform.Translate(new Vector3(0, -magnitude, 0)* Time.deltaTime );
//			return true;
//		} else {
//			return false;	
//		}
		gameObject.transform.Translate(new Vector3(0, -magnitude, 0)* Time.deltaTime );
		return true;
	}
	
	public bool moveCloudRight(float magnitude) {
//		Vector3 right = transform.TransformDirection(Vector3.right);
//		RaycastHit hit;
//		bool isHit = Physics.Raycast(transform.position, right, out hit);
//		if(isHit && hit.collider.tag != "Ground" || hit.distance > 1)  {
//			gameObject.transform.Translate(new Vector3(magnitude, 0, 0)* Time.deltaTime );
//			return true;
//		} else {
//			return false;
//		}
		gameObject.transform.Translate(new Vector3(magnitude, 0, 0)* Time.deltaTime );
		return true;
	}
	
	public bool moveCloudLeft(float magnitude) {
//		Vector3 left = transform.TransformDirection(Vector3.left);
//		RaycastHit hit;
//		bool isHit = Physics.Raycast(transform.position, left, out hit);
//		if(isHit  && hit.collider.tag != "Ground" || hit.distance > 1) {
//			gameObject.transform.Translate(new Vector3(-magnitude, 0, 0)* Time.deltaTime );
//			return true;
//		} else {
//			return false;	
//		}
		gameObject.transform.Translate(new Vector3(-magnitude, 0, 0)* Time.deltaTime );
		return true;
	}

	
	public void moveCloud(Vector3 move) {
		if(move.x > 0) {
			moveCloudRight(move.x);
		} else {
			moveCloudLeft(move.x*-1);
		}
		
		if(move.y > 0) { 
			moveCloudDown(move.y);
		} else {
			moveCloudUp(move.y*-1);
		}
	}
	
//	void OnCollisionExit(Collision c) {
//		gameObject.rigidbody.velocity = new Vector3(0f, 0f, 0f);	
//	}
	
}
