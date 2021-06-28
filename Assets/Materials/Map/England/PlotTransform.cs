using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotTransform : MonoBehaviour
{
    public float speed;
    public int lenghtOfPatrol;
    private float leftLimit, rightLimit;
    private bool move;

    void Start()
    {
        leftLimit = transform.position.x - lenghtOfPatrol;
        rightLimit = transform.position.x + lenghtOfPatrol;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > rightLimit)
            move = false;
        else if (transform.position.x < leftLimit)
            move = true;

        if (move)
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }
}
