using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
	public GUILayer guiLayer;
	// Use this for initialization
	void Start ()
	{
		guiLayer = Camera.main.GetComponent<GUILayer>();
	}

	// Update is called once per frame
	void Update ()
	{
		foreach (Touch touch in Input.touches) 
		{
			GUIElement hit = guiLayer.HitTest(touch.position);
			if (hit != null && hit.GetComponent<Touchable>()!= null)
			{
				Touchable touchable = hit.GetComponent<Touchable>();
				switch (touch.phase){
					case TouchPhase.Began:
						touchable.OnTouchStart();
					break;
					case TouchPhase.Moved:
					case TouchPhase.Stationary:
						touchable.OnTouch();
					break;
					case TouchPhase.Ended:
						touchable.OnTouchEnd();
				break;
			}
			}
		}
	}
}
