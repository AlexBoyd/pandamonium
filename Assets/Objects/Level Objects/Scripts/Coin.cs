using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public string id;
	private string levelName;
	private CoinContainer coinContainer;
	
	void Start(){
		coinContainer = transform.parent.GetComponent<CoinContainer>();
	}
	
	void Update() {
		transform.Rotate(Vector3.up * Time.deltaTime, 2f);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			coinContainer.consumedCoins.Add(id);
			print("Coin");
			Destroy(gameObject);
		}
	}
	
	
}
