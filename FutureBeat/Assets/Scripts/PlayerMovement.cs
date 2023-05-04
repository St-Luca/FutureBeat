using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0.05f;
    public Pos pos;
    public int gravity;

    void Start()
    {
        // GameObject.Find("Player").transform.position = new Vector3(-206.5f, -204.5f, 0);
        //GameObject.Find("Player").transform.position = new Vector3(0, 0, 0);
    }

    public void Update()
    {
        if(gravity == 0)
        {
            transform.position += new Vector3(speed, 0,0) * Input.GetAxis("Horizontal");
            transform.position += new Vector3(0, speed, 0) * Input.GetAxis("Vertical");

        }

        else
        {
            transform.position += new Vector3(speed, 0, 0) * Input.GetAxis("Horizontal");
        }
        //GameObject.Find("Main Camera").transform.position = GameObject.Find("Player").transform.position;
    }
}
