using UnityEngine;
using System.Collections;

public class FadeOutOnCollision : MonoBehaviour 
{
	bool StartFade = false;
	public float fade;

	
	void OnCollisionEnter(Collision collision)
	{
		
		print("Collision: " + collision.collider.gameObject.tag + " obj: " + gameObject.name);
		
		if(collision.collider.tag == "Player" && gameObject.tag == "Ground")
		{
			StartCoroutine(FadeCoroutine());
		}
		
		else if(collision.collider.tag != "Player" && gameObject.tag == "Spike" && gameObject.rigidbody.velocity.y < 0)
		{
			StartCoroutine(FadeCoroutine());
		}
		else if(collision.collider.tag == "Honey_Badger_Boss" && gameObject.tag == "Spike")
		{
			gameObject.active = false;
		}
		else if(collision.collider.tag == "Player" && gameObject.tag == "Heart")
		{
			print("Collision with heart");
			if(!SaveManager.IsAngerUnlocked()) {
				SaveManager.addAngerAbility();
			} else if(!SaveManager.IsHappinessUnlocked()) {
				SaveManager.addHappinessAbility();	
			} else if(!SaveManager.IsCourageUnlocked()) {
				SaveManager.addCourageAbility();
			}
			StartCoroutine(FadeCoroutine());
		}
		
		
		
	}
	
	public IEnumerator FadeCoroutine()
	{
		while(renderer.material.color.a > 0)
		{
			print("fading");
			renderer.material.SetAlpha(renderer.material.color.a - fade);
			yield return new WaitForFixedUpdate();
		}
		gameObject.active = false;
	}		
}