using UnityEngine;
using System.Collections;

public class CollectCoin : MonoBehaviour {
	
	public GUIText display;
	private int golds;
	public AudioSource coin;
	
	// Use this for initialization
	void Start () {
		golds = SaveManager.GetGoldCount();
		
		display.text = "Coins: " + golds.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnTriggerEnter(Collider other){
		if (other.tag == "Coin"){
			coin.Play();
			golds++;
			display.text = "Coins: " + golds.ToString();
			//display.text = SaveManager.getGold().ToString();
		}
	}
}
