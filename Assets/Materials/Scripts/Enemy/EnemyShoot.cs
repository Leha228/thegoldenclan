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

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
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
        GameObject newArrow = Instantiate(arrow, shootPoint.position, shootPoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }

}
