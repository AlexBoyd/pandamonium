using UnityEngine;
using System.Collections;

public class ToxicBarrelThrow : MonoBehaviour {

    Vector3 startLoc;
    Vector3 pandaLoc;
	// Use this for initialization
	void Start () {
        GameObject panda = GameObject.Find("CHAR_Panda");
        pandaLoc = panda.transform.position;
        startLoc = rigidbody.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(getTranslate());
        
	}

    Vector3 getTranslate()
    {
        float x = (pandaLoc.x - startLoc.x) * .6f * Time.deltaTime;
        float y = (pandaLoc.y - startLoc.y) * .6f * Time.deltaTime;
        return new Vector3(x, y, 0);
    }
}
