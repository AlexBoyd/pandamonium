using UnityEngine;
using System.Collections;

public class DirtboxSwitcher : MonoBehaviour {
	
	public Material HappyBox;
	public Material AngryBox;
	public Material CourageDarkSkyBox;
	public AudioSource music;
	
	// Use this for initialization
	void Start () {
		LevelRoot root = (LevelRoot) GameObject.Find(Application.loadedLevelName).GetComponent("LevelRoot");
		
		if(root.isAngry()){
			GameObject.Find("PandaCam").GetComponent<Skybox>().material = AngryBox;
		}
		
		if(root.isHappy()){
			GameObject.Find("PandaCam").GetComponent<Skybox>().material = HappyBox;
		}
		
		if(root.isDark()) {
			print("oh noes");
			GameObject.Find("PandaCam").GetComponent<Skybox>().material = CourageDarkSkyBox;
			GameObject.Find("PandaLight").GetComponent<Light>().enabled = false;
			Color ambient = new Color(0.05f, 0.05f, 0.05f, 0);
			RenderSettings.ambientLight = ambient;	
		}
		
		if(!root.isBoss()) {
			music.Play();
			music.loop = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
