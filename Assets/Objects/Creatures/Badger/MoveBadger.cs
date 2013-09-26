
using UnityEngine;
using System.Collections;

public class MoveBadger : MonoBehaviour
{
	
	//Badger moves back and forth a fixed distance of MOVE_DIST
	public double MOVE_DIST;
	public float SPEED = 3f;
	public GameObject badger;
	private float originalPosition = 0f;
	private bool movingRight = false;
	private bool isFacingRight = true;
	private bool isScared = false;

	private AnimationState walk;
	
	// Use this for initialization
	void Start ()
	{

		//animation.wrapMode = WrapMode.Loop;
		/*walk = animation["walk"];
		walk.wrapMode = WrapMode.Loop;
		originalPosition = rigidbody.position.x;
         */
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isScared) {
			doRunAway();
		} else {
			doDefaultMove();
		}

		//walk.enabled = true;
		//walk.weight = 1.0f;

	}
	
	void moveRight ()
	{
		//rigidbody.velocity = new Vector3 (3, rigidbody.velocity.y, rigidbody.velocity.z);
		if(!movingRight){
			transform.Rotate(new Vector3(0,180,0));
			movingRight = true;
		}
		rigidbody.velocity = new Vector3 (SPEED, rigidbody.velocity.y, 0);
		//badger.animation.Play("walk");	
	}
	
	void moveLeft ()
	{
		//rigidbody.velocity = new Vector3 (-3, rigidbody.velocity.y, rigidbody.velocity.z);
		if(movingRight){
			transform.Rotate(new Vector3(0,180,0));
			movingRight = false;
		}
		rigidbody.velocity = new Vector3 (SPEED * -1, rigidbody.velocity.y, 0);
		//badger.animation.Play("walk");
	}
	
	public void addScared ()
	{
		print("Badger is scared");
		isScared = true;
	}
	
	public void removeScared ()
	{
		isScared = false;
	}
	
	private void doRunAway ()
	{
		GameObject panda = GameObject.FindWithTag ("Panda");
		print("Badger dist: " + Vector3.Distance(panda.transform.position, transform.position));
		if (Vector3.Distance(panda.transform.position, transform.position) < 300) {
			//Only modify the behavior if the panda is within range of the badger
			if (panda.transform.position.x < transform.position.x) {
				//Panda is to the left of the badger
				print("Run right px: " + panda.transform.position.x + " bx: " + transform.position.x);
				moveRight();
				return;
			} else {
				print("Run left px: " + panda.transform.position.x + " bx: " + transform.position.x);
				//Panda is to the right of the badger	
				moveLeft();
				return;
			}
		}
		//If we're not in range, then we can just do the normal badger movement
		doDefaultMove();
	}
	
	
	private void doDefaultMove() {
		if (Mathf.Abs (rigidbody.velocity.x) < SPEED / 2) {
			if (movingRight) {
				moveLeft ();	
			} else {
				moveRight ();	
			}
		} else if (rigidbody.position.x > originalPosition + MOVE_DIST || !movingRight) {
			moveLeft ();	
		} else if (rigidbody.position.x < originalPosition || movingRight) {
			moveRight ();	
		}	
	}
	
	void OnCollisionEnter (Collision c)
	{
		
	}
	
}
