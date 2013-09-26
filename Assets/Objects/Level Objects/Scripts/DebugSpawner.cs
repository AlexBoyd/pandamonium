using UnityEngine;
using System.Collections;

public class DebugSpawner : MonoBehaviour {
	
	public Checkpoint checkpoint;
	public GameObject loaderPrefab;
	public bool debugSpawn = false;
	// Use this for initialization
	void Start () 
	{
		StartCoroutine(debugSpawner());		
	}
	
	private IEnumerator debugSpawner()
	{
		if(!debugSpawn){
			yield break;
		}
		yield return new WaitForFixedUpdate ();
		if(GameObject.FindGameObjectWithTag("Panda") == null)	
		{
			SaveManager.SaveCheckpointData(checkpoint);
			GameObject loader = Instantiate(loaderPrefab) as GameObject;
			loader.GetComponent<Loading>().Load();
		}
	}
}

