using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
using System;

public class Arrow : MonoBehaviour
{

    Rigidbody2D rb;
    private bool hasHit;
    GameObject enemy;

    public int damage = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GameObject.Find("Enemy");

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        rotate();
    }

    private void rotate()
    {
        if (!hasHit)
        {
            var dir = rb.velocity;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player") return;

        hasHit = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

}
