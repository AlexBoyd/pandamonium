using UnityEngine;
using System.Collections;

public class JumpTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Honey_Badger_Boss")
        {
            other.GetComponent<BadgerBoss>().Jump();
        }
    }
}
