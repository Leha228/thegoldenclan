
using UnityEngine;

public class PlotTransform : MonoBehaviour
{
    public float speed;
    public int lenOfPatrol;
    private float _leftLimit, _rightLimit;
    private bool _move;

    void Start()
    {
        _leftLimit = transform.position.x - lenOfPatrol;
        _rightLimit = transform.position.x + lenOfPatrol;
    }

    void Update()
    {
        if (transform.position.x > _rightLimit)
            _move = false;
        else if (transform.position.x < _leftLimit)
            _move = true;

        if (_move)
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }
}
