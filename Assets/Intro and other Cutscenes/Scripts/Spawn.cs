using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
	public GameObject[] cells;
	public GameObject loader;
	private int gameState = 0;
	private GUITexture skipButton;
	private int numScenes;
	private bool isIntroOver = false;

	// Use this for initialization
	void Start ()
	{
		GameObject skip = GameObject.Find ("skip");
		skipButton = (GUITexture)skip.GetComponent (typeof(GUITexture));
		numScenes = cells.Length;
	}

	// Update is called once per frame
	void Update ()
	{
		if (!isIntroOver) {
			if (Input.GetMouseButtonDown (0) && skipButton.HitTest (Input.mousePosition)) {
				for (int i = gameState; i < numScenes; i++) {
					DestroyImmediate (cells[i], true);
				}
				gameState = numScenes + 1;
			}
			
			if (Input.GetMouseButtonDown (0)) {
				//if gamestate > 0 hide last thing
				if (gameState > 0) {
					Destroy (cells[gameState - 1]);
				}
				if (gameState < numScenes){
					//instantiate new thing at this position
					cells[gameState] = (GameObject)Instantiate (cells[gameState], transform.position, Quaternion.identity);
				}
				//increment gamestate if < NUM_OF_SCENES
				if (gameState <= numScenes) {
					gameState++;
				}
			}
			
			if (gameState > numScenes) {
				loader.GetComponent<Loading>().Load();
				isIntroOver = true;
			}
		}	
	}
}
