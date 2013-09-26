using UnityEngine;
using System.Collections;

public class TrapDoor : Triggerable {
	
	private float fade = 0.1f;
	public override void Trigger()
	{
		StartCoroutine(FadeCoutine());	
	}
	
	public override void UnTrigger()
	{
		StopAllCoroutines();
		gameObject.active = true;
		StartCoroutine(RespawnCoroutine());	
	}
	
	
	private IEnumerator FadeCoutine()
	{
		while(renderer.material.color.a > 0)
		{
			renderer.material.SetAlpha(renderer.material.color.a - fade);
			if(!(renderer.material.color.a > 0))
			{
				gameObject.active = false;
			}
			yield return new WaitForFixedUpdate(); 
		}
	}
	
	private IEnumerator RespawnCoroutine()
	{
		while(renderer.material.color.a < 1)
		{
			renderer.material.SetAlpha(renderer.material.color.a + fade);
			yield return new WaitForFixedUpdate(); 
		}
		
	}
}
