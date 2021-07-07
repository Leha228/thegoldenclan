
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public static Arrow singleton { get; private set; }

    Rigidbody2D rb;
    private bool _hasHit;
    public int damage = 10;

    private void Awake() {
        singleton = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (!_hasHit)
        {
            var dir = rb.velocity;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player") return;

        _hasHit = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

}
