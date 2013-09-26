using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
	
	public float Velocity;
	public float DistanceToTravel;
	public bool isUpDown;
	public bool isContinuous;
	
	private Vector3 startLocation;
	private bool isMovingRight;
	
	
	void Start () 
	{
		startLocation = transform.position;
		if(isUpDown) {
			gameObject.rigidbody.constraints  = gameObject.rigidbody.constraints|RigidbodyConstraints.FreezePositionX;	
		} else {
			gameObject.rigidbody.constraints  = gameObject.rigidbody.constraints|RigidbodyConstraints.FreezePositionY;	
		}
	}
	
	void moveRight() 
	{
		moveRight(0);
	}
	
	void moveRight(float extraDistance)
	{
		transform.Translate(new Vector3(Velocity * Time.deltaTime + extraDistance, 0, 0));
	}
	
	void moveLeft() 
	{
		moveLeft(0);
	}
	
	void moveLeft(float extraDistance)
	{
		transform.Translate(new Vector3(-Velocity * Time.deltaTime + extraDistance, 0, 0));
	}
	
	
	//MOVING UP AND DOWN.......ruhhhhhh
	void moveUp() 
	{
		moveUp(0);
	}
	
	void moveUp(float extraDistance)
	{
		transform.Translate(new Vector3(0, Velocity * Time.deltaTime + extraDistance, 0));
	}
	
	void moveDown() 
	{
		moveDown(0);
	}
	
	void moveDown(float extraDistance)
	{
		transform.Translate(new Vector3(0, -Velocity * Time.deltaTime + extraDistance, 0));
	}
	
	
	void Update () 
	{
		if(isContinuous){
			if(isUpDown) {
				if (isMovingRight)
				{
					if(transform.position.y <= startLocation.y)
					{
						moveUp(0); //this will ensure if framerate is very low the platform won't go too far
						isMovingRight = false;
					}
					else 
					{
						moveDown();
					}
				}
				else
				{
					if (transform.position.y - DistanceToTravel >= startLocation.y)
					{
						moveDown(0); //this will ensure if framerate is very low the platform won't go too far
						isMovingRight = true;
					}
					else
					{
						moveUp();
					}
				}
				
			} else {
				if (isMovingRight)
				{
					if(transform.position.x - DistanceToTravel > startLocation.x)
					{
						moveLeft(transform.position.x - (DistanceToTravel + startLocation.x)); //this will ensure if framerate is very low the platform won't go too far
						isMovingRight = false;
					}
					else 
					{
						moveRight();
					}
				}
				else
				{
					if (transform.position.x < startLocation.x)
					{
						moveRight(startLocation.x - transform.position.x); //this will ensure if framerate is very low the platform won't go too far
						isMovingRight = true;
					}
					else
					{
						moveLeft();
					}
				}
			}
		} else {
			if(isUpDown) {
				if (isMovingRight)
				{
					if(transform.position.y < startLocation.y)
					{
						moveUp(0); //this will ensure if framerate is very low the platform won't go too far
						isMovingRight = false;
					}
					else if(transform.position.y == startLocation.y){
						transform.Translate(startLocation);
					}
					else 
					{
						moveDown();
					}
				}
				else
				{
					if (transform.position.y - DistanceToTravel > startLocation.y)
					{
						moveDown(0); //this will ensure if framerate is very low the platform won't go too far
						isMovingRight = true;
					}
					
					else
					{
						moveUp();
					}
				}
				
			} else {
				if (isMovingRight)
				{
					if(transform.position.x - DistanceToTravel > startLocation.x)
					{
						moveLeft(transform.position.x - (DistanceToTravel + startLocation.x)); //this will ensure if framerate is very low the platform won't go too far
						isMovingRight = false;
					}
					else 
					{
						moveRight();
					}
				}
				else
				{
					if (transform.position.x < startLocation.x)
					{
						moveRight(startLocation.x - transform.position.x); //this will ensure if framerate is very low the platform won't go too far
						isMovingRight = true;
					}
					else
					{
						moveLeft();
					}
				}
			}
		}
	}
}
