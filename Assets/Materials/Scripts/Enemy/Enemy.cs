
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float speed;
    public int lenOfPatrol;
    public Transform pointEnemy;
    private bool _move;

    public int lives = 50;

    void Start()
    {

    }

    
    void Update()
    {
        //if (Vector2.Distance(transform.position, pointEnemy.position) < lenghtOfPatrol)
            //Chill();
    }

    private void Chill()
    {
        if (transform.position.x > pointEnemy.position.x + lenOfPatrol)
            _move = false;
        else if (transform.position.x < pointEnemy.position.x - lenOfPatrol)
            _move = true;

        if (_move)
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name != "arrow(Clone)") return;
       
        lives -= Arrow.singleton.damage;
        Debug.Log(lives);
    }
}
