using UnityEngine;
using System.Collections;

public class MoveSpider : Triggerable {
	
	void OnCollisionEnter(Collision collision){
		if(collision.collider.tag != "Player" && (rigidbody.velocity.y < 0)){
			moveUp();
		}
	}
	
	void Update() {
		if (rigidbody.velocity.y < 0 && rigidbody.velocity.y > -8)
		{
			print("stop");
			rigidbody.velocity = Vector3.zero;	
		}
	}
	
	void moveDown() {
		rigidbody.velocity = new Vector3(0, -8, 0);
	}
	
	void moveUp() {
		rigidbody.velocity = new Vector3(0, 3, 0);
	}
	
	public override void Trigger()
	{
		if (rigidbody.velocity.y == 0)
		moveDown();
	}
}
