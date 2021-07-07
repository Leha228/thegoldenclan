using System;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrow;
    public float launchForce = 5;
    public Transform shootPoint;

    public GameObject point;
    GameObject[] points;
    public int numberOfPoints = 5;
    public float spaceBetweenPoint;
    private bool _createPoints = false;

    void Start()
    {
        points = new GameObject[numberOfPoints];
        _createPoints = true;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 bowPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePosition - bowPosition;
        transform.right = dir;

        if (Input.GetButton("Fire2"))
        {
            if (Math.Round(Vector2.Distance(bowPosition, mousePosition)) >= 5 && Math.Round(Vector2.Distance(bowPosition, mousePosition)) <= 12)  
                launchForce = Vector2.Distance(bowPosition, mousePosition); 
            
            if (_createPoints)
            {
                for (int i = 0; i < numberOfPoints; i++)
                {
                    points[i] = Instantiate(point, shootPoint.position, Quaternion.identity);
                }
                _createPoints = false;
            }

            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].SetActive(true);
                points[i].transform.position = PointPosition(i * spaceBetweenPoint);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                //Invoke(nameof(shoot), 0.5f);
                Shoot();
            }
        } 
        else
        {
            if (!_createPoints)
            {
                for (int i = 0; i < numberOfPoints; i++)
                {
                    points[i].SetActive(false);
                }
            }
        }
    }

    private void Shoot()
    {
        GameObject newArrow = Instantiate(arrow, shootPoint.position, shootPoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }

    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)shootPoint.position +
            (Vector2)(transform.right * launchForce * t) + (Physics2D.gravity * (t * t) * 0.5f);
        return position;
    }

}
