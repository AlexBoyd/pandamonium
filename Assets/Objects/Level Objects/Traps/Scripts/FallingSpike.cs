using UnityEngine;
using System.Collections;

public class FallingSpike : Triggerable {

	void moveDown() {
		rigidbody.velocity = new Vector3(0, -8, 0);
	}
		
	public override void Trigger()
	{
		moveDown();
	}
}
