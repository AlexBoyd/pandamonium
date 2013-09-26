using UnityEngine;
using System.Collections;

public class Courage : MonoBehaviour
{
	
	private bool isActiveCourage = false;
	private bool isPassiveCourage = false;
	public bool isCourage = false;
	public AudioSource sound;
	public GameObject courageLight;
	private GameObject myCourageLight;
	
	public Material darkSkyBox;
	public Material lightSkyBox;
	public Material skyBox;
	public GameObject hint;

	
	// Update is called once per frame
	void Update () {
		if(myCourageLight != null) {
			Vector3 pos = gameObject.transform.position;
			pos.z -= 1.5f;
			myCourageLight.transform.position = pos;
		}
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Courage" && SaveManager.IsCourageUnlocked()) {
			addCourage ();
			Destroy (other.gameObject);
			sound.Play();
		}
		else if (other.tag == "Courage" && !SaveManager.IsCourageUnlocked()){
			hint.GetComponent<GUITexture>().enabled = true;
			StartCoroutine("disableTimer");
		}
	}
	
	IEnumerator disableTimer()
	{
		yield return new WaitForSeconds(1.5f);
		hint.GetComponent<GUITexture>().enabled = false;
	}
	
	
	public bool isCouragous() {
		return isCourage;	
	}
	
	private void addCourage ()
	{
			Courage courage = gameObject.GetComponent<Courage> ();
			if(!courage.isCourage) {
		
				LevelRoot root = (LevelRoot) GameObject.Find(Application.loadedLevelName).GetComponent("LevelRoot");
	
				if(root.isDark()) {
					GameObject.Find("PandaCam").GetComponent<Skybox>().material = lightSkyBox;
					RenderSettings.ambientLight = Color.black;
				}
			
				isActiveCourage = true;
				isPassiveCourage = true;
				isCourage = true;
		
				//Create the courage halo
				GameObject halo = GameObject.Find("CourageHalo");
		   		(halo.GetComponent("Halo") as Behaviour).enabled = true;
			
				//Create the light
				myCourageLight = Instantiate (courageLight) as GameObject;
	
				//Remove the other abilities
				Happy happy = gameObject.GetComponent<Happy> ();
				if(happy.isHappy) {
					happy.removeHappy();
				}
				Anger anger = gameObject.GetComponent<Anger> ();
				if(anger.isAngry) {
					anger.removeAnger ();
				}
			}	
	}
	
	public void removeCourage ()
	{
		LevelRoot root = (LevelRoot) GameObject.Find(Application.loadedLevelName).GetComponent("LevelRoot");
		print(root.isDark());
		if(root.isDark()) {
			GameObject.Find("PandaCam").GetComponent<Skybox>().material = darkSkyBox;
			Color ambient = new Color(0.05f, 0.05f, 0.05f, 0);
			RenderSettings.ambientLight = ambient;
		}

		isActiveCourage = false;
		isPassiveCourage = false;
		isCourage = false;
		//Remove the halo
		GameObject halo = GameObject.Find("CourageHalo");
	    (halo.GetComponent("Halo") as Behaviour).enabled = false;
		myCourageLight.active = false;
		Destroy(myCourageLight);
		
	}
	
}
