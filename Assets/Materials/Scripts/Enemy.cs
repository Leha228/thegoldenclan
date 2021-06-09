using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int lives = 50;
    public GameObject arrow;
    int damage;

    void Start()
    {
        damage = arrow.GetComponent<Arrow>().damage;
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name != "arrow(Clone)") return;
       
        lives -= damage;
        Debug.Log(lives);
    }
}
