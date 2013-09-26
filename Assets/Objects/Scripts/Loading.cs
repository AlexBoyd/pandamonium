using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Loading : MonoBehaviour {

	public void Load()
	{
		StartCoroutine(loadCoRoutine());
	}
	public void LoadElevator(string elevatorLevelName)
	{
		StartCoroutine(LoadElevatorCoroutine(elevatorLevelName));	
	}
	private IEnumerator loadCoRoutine(){
		DontDestroyOnLoad(gameObject);
		KeyValuePair<string,string> checkpointData = SaveManager.LoadCheckpointData();
		yield return StartCoroutine(LevelManager.LoadCheckpointCoroutine(checkpointData.Key,checkpointData.Value));
		Destroy(gameObject);
	}
	public IEnumerator LoadElevatorCoroutine(string elevatorLevelName)
	{
		DontDestroyOnLoad(gameObject);
		yield return StartCoroutine(LevelManager.LoadElevatorCheckPoint(elevatorLevelName));
		Destroy(gameObject);
	}
}
