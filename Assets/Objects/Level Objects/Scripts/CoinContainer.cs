using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CoinContainer : MonoBehaviour {
	
	
	public List<string> consumedCoins = new List<string>();
	private string levelName;
	
	
	void Start()
	{
		levelName = transform.parent.name;
	}
	
	public void SaveCoins()
	{
		foreach(string coin in consumedCoins)
		{
			print(coin);	
		}
		SaveManager.nuggetDict[levelName] = consumedCoins;
	}
	
	void OnDestroy()
	{
		if(SaveManager.nuggetDict.ContainsKey(levelName))
		{
			SaveManager.nuggetDict[levelName].Clear();	
		}
	}
}
