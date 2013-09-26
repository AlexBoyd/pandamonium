using UnityEngine;
using System.Collections;
using System;

public class BadgerBoss : Triggerable
{

    // Use this for initialization
    void Start()
    {
        animation.wrapMode = WrapMode.Loop;
    }

    GameObject panda = null;
    bool goingRight = true;
    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = rigidbody.velocity;
        if (shouldGoRight())
        {
            rigidbody.velocity = new Vector3(9.5f + speedIncrease(), velocity.y, velocity.z);
            if (!goingRight)
            {
                transform.Rotate(new Vector3(0, 180, 0));
            }
            goingRight = true;
        }
        else
        {
            rigidbody.velocity = new Vector3(-9.5f + speedIncrease(), velocity.y, velocity.z);
            if (goingRight)
            {
                transform.Rotate(new Vector3(0, 180, 0));
            }
            goingRight = false;
        }
    }

    float speedIncrease()
    {
        float distance = GameObject.FindGameObjectWithTag("Player").rigidbody.position.x - rigidbody.position.x;
        System.Random r = new System.Random(Convert.ToInt32((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds%142416182));
        double random = r.Next(0, 2);
        if (distance> 8)
        {
            return (float)random * 3f;
        }
        else if (distance < -8)
        {
            return -((float)random * 3f);
        }
        return (float)random;
    }

    bool shouldGoRight()
    {
        if (GameObject.FindGameObjectWithTag("Player").rigidbody.position.x > rigidbody.position.x && (!pandaIsBelow() || goingRight))
        {
            return true;
        }
        else if (GameObject.FindGameObjectWithTag("Player").rigidbody.position.x < rigidbody.position.x && (!pandaIsBelow() || !goingRight))
        {
            return false;
        }
        return goingRight;
    }

    bool pandaIsBelow()
    {
        if (GameObject.FindGameObjectWithTag("Player").rigidbody.position.y < rigidbody.position.y)
        {
            return true;
        }
        return false;
    }

    public override void Trigger()
    {
		GetComponent<MeshCollider>().enabled = true;
		GetComponent<BadgerBoss>().enabled = true;
        GameObject badger = GameObject.Find("honeyBadger");
		badger.GetComponent<SkinnedMeshRenderer>().enabled = true;
        rigidbody.isKinematic = false;
    }

    public void Jump()
    {
        Vector3 velocity = rigidbody.velocity;
        if (goingRight)
        {
            rigidbody.velocity = new Vector3(velocity.x, 15, velocity.z);
        }
        else
        {
            rigidbody.velocity = new Vector3(velocity.x, 15, velocity.z);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Lava")
        {
            gameObject.SetActiveRecursively(false);
        }
    }
}
