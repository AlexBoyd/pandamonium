using UnityEngine;
using System.Collections;

public class ForegroundSwitcher : MonoBehaviour {

	public float angerColour = .4f;
	public float happyColour;
	public float courageColour;
	
	// Use this for initialization
	void Start () {
		LevelRoot root = (LevelRoot) GameObject.Find(Application.loadedLevelName).GetComponent("LevelRoot");
		Color colour = renderer.material.color;
		
		if(root.isAngry()){
			Color newColour = new Color(colour.r+angerColour, colour.g+angerColour, colour.b+angerColour, 1);
			renderer.material.SetColor("_Color", newColour);
		}
		
		if(root.isHappy()){
			//GameObject.Find("PandaCam").GetComponent<Skybox>().material = HappyBox;
		}
		
		if(root.isDark()) {
			//print("oh noes");
			//GameObject.Find("PandaCam").GetComponent<Skybox>().material = CourageDarkSkyBox;
			//GameObject.Find("PandaLight").GetComponent<Light>().enabled = false;
			//Color ambient = new Color(0.05f, 0.05f, 0.05f, 0);
			//RenderSettings.ambientLight = ambient;	
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
