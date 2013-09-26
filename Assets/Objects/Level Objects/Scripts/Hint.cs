using UnityEngine;
using System.Collections;

public class Hint : MonoBehaviour {
	
	public GameObject hint;
	
	void OnCollisionEnter(Collision collision)
	{
		if((collision.collider.tag == "p_box" || collision.collider.tag == "Cube") && !gameObject.GetComponent<Anger>().isAngry && 
			!(gameObject.rigidbody.velocity.y < 0) ) 
		{
			hint.GetComponent<GUITexture>().enabled = true;
			StartCoroutine("disableHintCoroutine");
		}
	}
	
	IEnumerator disableHintCoroutine()
	{
		yield return new WaitForSeconds(1f);
		hint.GetComponent<GUITexture>().enabled = false;
	}
}
