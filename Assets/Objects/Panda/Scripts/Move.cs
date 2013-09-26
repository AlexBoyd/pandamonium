using UnityEngine;
using System;
using System.Collections;
[RequireComponent(typeof(GUITexture))]

public class Move : MonoBehaviour
{
	public AudioSource jumpSound;
	public AudioSource sound;
	private int lastPandaDirectionX = 1;
	private bool isFacingRight = true;
//	private bool isJumping = false;
	
	private float DEFAULT_SPEED = 10f;
	private float pandaSpeed = 10f;

	private AnimationState jumpAnim;
	
	//Variables for jumping
	public float mGroundCastLength = 3;
	public GameObject pandaModel;
	public IsGrounded groundChecker;
	private float courageCoolOff = 2f;
//
//	//Coordinates for the Move Button GUITexture
//	private float move_rectMid;
//	private float move_leftMid;
//	private float move_rightMid;
//	private float move_leftRect;
//	private float move_rightRect;
//
//	//Coordinates for the Action button GUITexture
//	private float action_rectMid;
//	private float action_leftMid;
//	private float action_rightMid;
//	private float action_leftRect;
//	private float action_rightRect;
//
	private GUITexture moveLeft;
	private GUITexture moveRight;
	private GUITexture xButton;
	private GUITexture oButton;
	private bool _buttonEnabled;
	private float msCount = 0f;
	



	// Use this for initialization
	void Start ()
	{
		GameObject mLeft = GameObject.Find ("left");
		GameObject mRight = GameObject.Find ("right");
		GameObject xB = GameObject.Find ("x_button");
		GameObject oB = GameObject.Find ("o_button");
		moveLeft = (GUITexture)mLeft.GetComponent (typeof(GUITexture));
		moveRight = (GUITexture)mRight.GetComponent (typeof(GUITexture));
		xButton = (GUITexture)xB.GetComponent (typeof(GUITexture));
		oButton = (GUITexture)oB.GetComponent (typeof(GUITexture));
		
		//jumpAnim = 
		pandaModel.animation["Jump"].wrapMode = WrapMode.ClampForever;

		//jumpAnim.blendMode = AnimationBlendMode.Additive;
		//jump.enabled = true;
		//jumpAnim.weight = 1.0f;
		//jumpAnim.wrapMode = WrapMode.ClampForever;

//		//Initialize the move button instance variables
//		Rect moveRect = arrowButtons.pixelInset;
//		move_rectMid = (float)(Screen.width * .07);
//		move_leftMid = move_rectMid - (moveRect.width / 6);
//		move_rightMid = move_rectMid + (moveRect.width / 6);
//		move_leftRect = move_rectMid + moveRect.xMin;
//		move_rightRect = move_rectMid + moveRect.xMax;
//		//Initialize the action button instance variables
//		Rect actionRect = actionButtons.pixelInset;
//		action_rectMid = (float)(Screen.width * .9);
//		action_rightMid = action_rectMid - (actionRect.width / 10);
//		action_leftMid = action_rectMid + (actionRect.width / 10);
//		action_leftRect = action_rectMid + actionRect.xMin;
//		action_rightRect = action_rectMid + actionRect.xMax;
		
	}


	void Update ()
	{
		msCount += Time.deltaTime;
		courageCoolOff += Time.deltaTime;
		
		if(msCount > 2 && pandaSpeed > DEFAULT_SPEED) {
			resetSpeed();
			msCount = 0;
		}
		
		bool isAndroid = false;

		
		if(Input.GetKeyUp(KeyCode.Escape)) {
			Application.LoadLevel("3D Menu");
			return;
		}
		
		foreach (Touch touch in Input.touches) {
			//do hit test, we 'press' it??
			switch (touch.phase) {
			case TouchPhase.Began:
				isAndroid = true;
				if (moveRight.HitTest (touch.position)) {
					hitRightButton ();
				} else if (moveLeft.HitTest (touch.position)) {
					hitLeftButton ();
				}
				
				if (xButton.HitTest (touch.position)) {
					hitXButton ();
				} else if (oButton.HitTest (touch.position)) {
					hitOButton();
					msCount = 0;
				}
				break;
			case TouchPhase.Moved:
			case TouchPhase.Stationary:
				if (moveRight.HitTest (touch.position)) {
					hitRightButton ();
				} else if (moveLeft.HitTest (touch.position)) {
					hitLeftButton ();
				} else if(!xButton.HitTest(touch.position) && !oButton.HitTest(touch.position)) {
					stopMovement();	
				}
				isAndroid = true;
				
				break;
			}
		}
		
		if (isAndroid) {
			return;
		}
		
				//PC TESTING STUFF
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			hitOButton();
		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			hitXButton ();
		}
		
		if (Input.GetKey (KeyCode.RightArrow) && Input.GetKey (KeyCode.LeftArrow)) {
			stopMovement ();
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			hitRightButton ();
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			hitLeftButton ();
		} else {
			stopMovement ();
		}

		if(!isGrounded()) pandaModel.animation.Play("Jump");
	}
	
	public void resetSpeed() {
		pandaSpeed = DEFAULT_SPEED;
		courageCoolOff = 0f;
	}
	
	public void increaseSpeed() {
		if(pandaSpeed == DEFAULT_SPEED) {
			msCount = 0;
			pandaSpeed *= 2f;
		}
		
	}


	//Move Right - give args for proporitional controls maybe later
	void hitRightButton ()
	{
		rigidbody.velocity = new Vector3 (pandaSpeed, rigidbody.velocity.y, rigidbody.velocity.z);
		lastPandaDirectionX = 1;
		walkRight();
	}

	//Move Left
	void hitLeftButton ()
	{
		rigidbody.velocity = new Vector3 (-pandaSpeed, rigidbody.velocity.y, rigidbody.velocity.z);
		walkLeft();
		lastPandaDirectionX = -1;
	}

	//Hit X
	void hitXButton ()
	{
		pandaModel.animation.Stop();
		if (isPassiveHappy()) {
			if (canDoubleJump ()) {
				rigidbody.velocity = new Vector3 (0, 15, 0);

				jump();
			}
		} else if (isGrounded()) {
			if(rigidbody.velocity.y < 10) {
				rigidbody.velocity = new Vector3 (0, 15, 0);
				Vector3 v = rigidbody.velocity;
				if(rigidbody.velocity.y > 15) {
					v = new Vector3(v.x, 15, v.z);	
				}
				jump();
			}
		}
	}
	
	
	public bool isGrounded() {
		return groundChecker.grounded;	
	}

	private bool usedDoubleJump = false;
	bool canDoubleJump ()
	{
		if (!usedDoubleJump) {
			usedDoubleJump = true;
			return true;
		}
		if (groundChecker.grounded) {
			usedDoubleJump = false;
			return true;
		}
		return false;
	}

	//Hit O
	void hitOButton ()
	{
		
		if (isActiveAngry()) {
			Anger pandaAnger = gameObject.GetComponent<Anger> ();
			pandaAnger.doAnger();
			sound.Play();
		}
		if (isActiveHappy()) {
			Happy h = gameObject.GetComponent<Happy>();
			h.Float();
			sound.Play();
		}
		if (isActiveCourage()) {
			print("increase speed");
			if(courageCoolOff > 2f) {
				increaseSpeed();
				sound.Play();
			}
		}
	}
			

	void stopMovement ()
	{
		rigidbody.velocity = new Vector3 (0, rigidbody.velocity.y, rigidbody.velocity.z);
		//pandaModel.animation.Stop("Walk");
		if(isGrounded()) pandaModel.animation.CrossFade("T");
	}


	void OnDrawGizmos ()
	{
		Gizmos.DrawLine (transform.position, transform.position + transform.up * -mGroundCastLength);
	}




	private bool isActiveHappy ()
	{
		Happy h = gameObject.GetComponent<Happy> ();
		return h.isPandaActiveHappy ();
	}

	private bool isPassiveHappy ()
	{
		Happy h = gameObject.GetComponent<Happy> ();
		return h.isPandaPassiveHappy ();
	}
	
	

	private bool isPassiveAngry ()
	{
		Anger a = gameObject.GetComponent<Anger> ();
		return a.isPandaPassiveAngry();
	}

	private bool isActiveAngry ()
	{
		Anger a = gameObject.GetComponent<Anger> ();
		return a.isPandaActiveAngry();
	}
	
	
	private bool isActiveCourage()
	{
		Courage c = gameObject.GetComponent<Courage> ();
		return c.isCouragous();
	}
	
	
	public int getLastPandaDirectionX() {
		return lastPandaDirectionX;	
	}
	
	void walkLeft(){
		if (isFacingRight){
			//pandaModel.transform.localRotation = new Quaternion(0,270,0, 0);
			pandaModel.transform.Rotate(new Vector3(0,180,0));
			isFacingRight=false;
		}
		if(!isGrounded()) return;
		pandaModel.animation.Play("Walk");
	}
	
	void walkRight(){
		if (!isFacingRight){ 
			//pandaModel.transform.localRotation = new Quaternion(0,90,0, 0);
			pandaModel.transform.Rotate(new Vector3(0,180,0));
			isFacingRight = true;
		}
		if(!isGrounded()) return;
		pandaModel.animation.Play("Walk");
	}
	
	void jump(){
		jumpSound.Play();
		pandaModel.animation.Stop();
		pandaModel.animation.Play("Jump");
		//jumpAnim.enabled = true;
	}
	
}
