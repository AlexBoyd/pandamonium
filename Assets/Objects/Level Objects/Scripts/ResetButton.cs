using UnityEngine;
using System.Collections;

public class ResetButton : MonoBehaviour {
	
	private GUITexture resetButton;
	public GameObject loaderPrefab;
	
	// Use this for initialization
	void Start () {
		GameObject mReset = GameObject.Find ("reset");
		resetButton = (GUITexture)mReset.GetComponent (typeof(GUITexture));
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.R)) {
			reset();
		}
		
		foreach (Touch touch in Input.touches) {
			//do hit test, we 'press' it??
			switch (touch.phase) {
			case TouchPhase.Ended:
				if (resetButton.HitTest (touch.position)) {
					reset();
				}
				break;
			}
		}
		
	}
	
	
	private void reset() {
		GameObject loader = Instantiate (loaderPrefab) as GameObject;
		loader.GetComponent<Loading> ().Load ();	
	}	
}
