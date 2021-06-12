using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject arrow;
    public GameObject player;
    public Transform shootPoint;

    private float launchForce;
    private bool movePlayer = true;

    void Start()
    {
 
    }

    [Obsolete]
    void Update()
    {
        move();
    }

    [Obsolete]
    private void move()
    {

        Vector2 bowPosition = transform.position;
        Vector2 playerPosition = player.transform.position;
        Vector2 dir = playerPosition - bowPosition;
        transform.right = dir;

        launchForce = Vector2.Distance(bowPosition, playerPosition);

        if (!movePlayer) return;
        movePlayer = false;
        Invoke(nameof(shoot), 3f);
    }

    private void shoot()
    {
        Vector2 bowPosition = transform.position;
        Vector2 playerPosition = player.transform.position;
        var dir = playerPosition - bowPosition;
        var euler = transform.eulerAngles;
        euler.z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 30.0f; //random
        transform.eulerAngles = euler;

        GameObject newArrow = Instantiate(arrow, shootPoint.position, transform.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }

}
