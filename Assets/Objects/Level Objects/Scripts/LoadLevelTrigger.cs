using UnityEngine;

public class LoadLevelTrigger : MonoBehaviour
{
    public string LevelName;
	public bool cutSceneLoad;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if(cutSceneLoad)
			{
				Application.LoadLevel(LevelName);	
			}
			else
			{
				LevelManager.LoadLevel(LevelName);
			}
        }
    }
}

