using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Checkpoint : MonoBehaviour {

	public GameObject pandaPrefab;
	public ParticleEmitter checkpointActive;
	public ParticleEmitter checkpointActivate;
	public List<string> levelsToLoad;
	
	void Update()
	{
		KeyValuePair<string, string> curCheckpoint = SaveManager.curCheckpoint;
		if (curCheckpoint.Key == transform.parent.name && curCheckpoint.Value == name)
		{
			checkpointActive.emit = true;
		}
		else
		{
			checkpointActive.emit = false;
		}	
	}
	
	public virtual void saveCheckpoint()
	{	
		CoinContainer[] coinContainers = FindObjectsOfType(typeof(CoinContainer)) as CoinContainer[];
		foreach(CoinContainer coins in coinContainers)
		{
			coins.SaveCoins();
		}
		SaveManager.SaveCheckpointData(this);
	}
	
	public void loadCheckpoint()
	{
		Instantiate(pandaPrefab,transform.position, Quaternion.identity);
		foreach (string s in levelsToLoad)
		{
			LevelManager.LoadLevel(s);	
		}
	}
	
	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Player")
		{
			audio.Play();
			saveCheckpoint();
			checkpointActivate.Emit();
		}
	}
	
	
}
