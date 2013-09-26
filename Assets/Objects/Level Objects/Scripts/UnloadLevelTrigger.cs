using UnityEngine;

public class UnloadLevelTrigger : MonoBehaviour
{
    public string LevelName;

    void OnTriggerEnter(Collider collider)
    {
		if (collider.tag == "Player")
        {
            LevelManager.UnloadLevel(LevelName);
        }
    }
}

