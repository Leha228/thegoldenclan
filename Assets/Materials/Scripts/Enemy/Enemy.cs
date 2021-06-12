using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public int lenghtOfPatrol;
    public Transform pointEnemy;
    private bool move;

    public int lives = 50;
    public GameObject arrow;
    int damage;

    void Start()
    {
        damage = arrow.GetComponent<Arrow>().damage;
    }

    
    void Update()
    {
        //if (Vector2.Distance(transform.position, pointEnemy.position) < lenghtOfPatrol)
            chill();
    }

    private void chill()
    {
        if (transform.position.x > pointEnemy.position.x + lenghtOfPatrol)
            move = false;
        else if (transform.position.x < pointEnemy.position.x - lenghtOfPatrol)
            move = true;

        if (move)
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }

    private void angry()
    {

    }

    private void goBack()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name != "arrow(Clone)") return;
       
        lives -= damage;
        Debug.Log(lives);
    }
}
