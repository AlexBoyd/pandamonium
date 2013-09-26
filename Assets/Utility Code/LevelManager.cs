using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class LevelManager
{

	public static List<string> loadedLevels = new List<string>();

	public static void LoadLevel (string levelToLoad)
	{
		if (!isLoaded (levelToLoad)) {
			Application.LoadLevelAdditiveAsync (levelToLoad);
			MonoBehaviour.print("loading level " + levelToLoad);
			loadedLevels.Add(levelToLoad);
			LoadCoins(levelToLoad);

		}
	}

	public static void UnloadLevel (string levelToUnload)
	{
		if (isLoaded (levelToUnload)) {
			GameObject obj = GameObject.Find (levelToUnload);
			CoinContainer coinContainer = obj.GetComponentInChildren<CoinContainer>();
			coinContainer.SaveCoins();
			GameObject.Destroy (obj);
			loadedLevels.Remove(levelToUnload);
		}
	}
	
	public static IEnumerator LoadElevatorCheckPoint(string levelName)
	{
		return LoadCheckpointCoroutine(levelName, "elevator");
	}
	
	public static IEnumerator LoadCheckpointCoroutine (string checkpointLevel, string checkpointName)
	{
		MonoBehaviour.print(checkpointLevel);
		loadedLevels.Clear();
		loadedLevels.Add(checkpointLevel);
		Application.LoadLevel (checkpointLevel);
		yield return new WaitForFixedUpdate ();
		Checkpoint checkpoint = GameObject.Find (checkpointName).GetComponent<Checkpoint> ();
		checkpoint.loadCheckpoint();
		LoadCoins(checkpointLevel);
		
	}
	public static void LoadCoins (string level)
	{
		List<string> nuggets = SaveManager.LoadGold(level);
		if (nuggets != null) 
		{
			GameObject levelObject = GameObject.Find(level);
			CoinContainer coinContainer = levelObject.GetComponentInChildren<CoinContainer> ();
			foreach (Coin coin in coinContainer.GetComponentsInChildren<Coin> ()) 
			{
				if (nuggets.Contains (coin.id)) 
				{
					coinContainer.consumedCoins.Add(coin.id);
					MonoBehaviour.Destroy (coin.gameObject);
				}
			}
		}
	}

	private static bool isLoaded (string levelName)
	{
		MonoBehaviour.print("checking level to load");
		return loadedLevels.Contains (levelName);
	}
}
