/*
using UnityEngine;
using System.Collections;

public class Tutorial : Touchable {
	
	public override void OnTouchStart()
	{
		guiText.material.color = Color.red;
		StartCoroutine(...);
	}

	private IEnumerator tutorialCoroutine(){

	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
*/

/*
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResumeJourney : Touchable
{
	public bool isQuit = false;
	public GameObject loadingSplash;
	
	public override void OnTouchStart()
	{
		guiText.material.color = Color.red;
	}
	
	public override void OnTouchEnd()
	{
		guiText.material.color = Color.white;
		
		if(isQuit)
		{
			Application.Quit();
		}
		else
		{
			loadingSplash.GetComponent<Loading>().Load();
		}
	}
	
	public override void OnTouch(){}
	                
}
 */

/*
 * using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Loading : MonoBehaviour {

	public void Load()
	{
		
		StartCoroutine(loadCoRoutine());
	}
	private IEnumerator loadCoRoutine(){
		DontDestroyOnLoad(gameObject);
		KeyValuePair<string,string> checkpointData = SaveManager.LoadCheckpointData();
		yield return StartCoroutine(LevelManager.LoadCheckpointCoroutine(checkpointData.Key,checkpointData.Value));
		Destroy(gameObject);
	}
}
 */ 
