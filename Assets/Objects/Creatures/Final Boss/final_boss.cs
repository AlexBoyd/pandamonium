using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;
using System.Collections.Generic;

public class final_boss : MonoBehaviour {

    Stopwatch sw = new Stopwatch();

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
    GameObject player = null;
    System.Random r = new System.Random(51223125);
    Stopwatch last_ground_removed = new Stopwatch();
    List<GameObject> spikes = new List<GameObject>();
    Dictionary<DateTime, GameObject> badgers = new Dictionary<DateTime, GameObject>();
    Stopwatch jumper = new Stopwatch();
    bool facingLeft = true;

	void Update () {

        handlejump();
        if (jumper.IsRunning)
        {
            animation.wrapMode = WrapMode.Once;
            animation.Play("moleking_jump");
        }
        else if (!animation.isPlaying)
        {
            animation.wrapMode = WrapMode.Loop;
            animation.Play("moleking_walk");
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (!sw.IsRunning && Vector3.Distance(player.rigidbody.position, rigidbody.position) < 20)
        {
            last_ground_removed.Start();
            sw.Start();
        }
        if (sw.IsRunning && sw.Elapsed.TotalMilliseconds > 2250)
        {
            sw.Reset();
            sw.Start();

            float distance = (float)Math.Sqrt(Math.Pow((double)(player.rigidbody.position.x - rigidbody.position.x), 2));

            int direction = 1;
            if (player.rigidbody.position.x < rigidbody.position.x)
            {
                direction = -1;
            }
            if (direction == -1 && !facingLeft)
            {
                rigidbody.MoveRotation(new Quaternion(0, 60, 0, 0));
            }
            else if (direction == 1 && facingLeft)
            {
                rigidbody.MoveRotation(new Quaternion(0, -60, 0, 0));
            }
            
            switch (r.Next(0,4))
            {
                case 0:
                    jumper.Start();
                    break;
                case 1:
                case 2:
                    GameObject spike = GameObject.FindGameObjectWithTag("Spike");
                    GameObject newspike = (GameObject)GameObject.Instantiate((GameObject)spike);
                    newspike.rigidbody.position = new Vector3(rigidbody.position.x + (direction * 4f), -.75f, 0);
                    print("Location: " + (rigidbody.position.y - 1));
                    newspike.rigidbody.MoveRotation(new Quaternion(90, direction * -1 * 90, 0, 0));
                    newspike.rigidbody.useGravity = false;
                    newspike.rigidbody.velocity = new Vector3(direction * 10, 0, 0);
                    newspike.rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                    spikes.Add(newspike);
                    break;
                case 3:
                    print("Badger");
                    GameObject badger = GameObject.FindGameObjectWithTag("Badger");
                    GameObject newbadger = (GameObject)GameObject.Instantiate((GameObject)badger);
                    newbadger.rigidbody.position = new Vector3(rigidbody.position.x + (direction * 3.5f), rigidbody.position.y+4, 0);
                    last_ground_removed.Reset();
                    last_ground_removed.Start();
                    badgers.Add(DateTime.Now, newbadger);
                    break;
            }
        }
        for (int i = spikes.Count-1; i >=0; i--)
        {
            if (spikes[i].rigidbody.velocity.x != 10 && spikes[i].rigidbody.velocity.x != -10)
            {
                print("Speed " + spikes[i].rigidbody.velocity.x);
                Destroy(spikes[i]);
                spikes.RemoveAt(i);
            }
        }

        List<DateTime> toRemove = new List<DateTime>();
        foreach (KeyValuePair<DateTime, GameObject> obj in badgers)
        {
            if ((DateTime.Now - obj.Key).TotalMilliseconds > 10000)
            {
                toRemove.Add(obj.Key);
                Destroy(obj.Value);
            }
        }
        foreach (DateTime t in toRemove)
        {
            badgers.Remove(t);
        }
	}



    void handlejump()
    {
        if (jumper.Elapsed.TotalMilliseconds > 200)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            float distance = (float)Math.Sqrt(Math.Pow((double)(player.rigidbody.position.x - rigidbody.position.x), 2));

            int direction = 1;
            if (player.rigidbody.position.x < rigidbody.position.x)
            {
                direction = -1;
            }
            rigidbody.velocity = new Vector3((float)(Math.Pow(Math.Sqrt(distance), 1.35) * 1.475 * direction + (direction)), distance + (float)Math.Sqrt(distance) + 3.5f, 0);
            jumper.Reset();
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "final_boss_ground" && last_ground_removed.Elapsed.TotalMilliseconds > 2000 && rigidbody.velocity.y > -2)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            collision.collider.gameObject.SetActiveRecursively(false);
            last_ground_removed.Reset();
            last_ground_removed.Start();
        }
        else if (collision.collider.tag != "Badger" && rigidbody.position.y < 10)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        if (collision.collider.tag == "Lava")
        {
            gameObject.SetActiveRecursively(false);
            Application.LoadLevel("Cutscene8");
        }
        
    }
}
