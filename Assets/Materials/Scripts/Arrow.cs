using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class Arrow : MonoBehaviour
{

    private SkeletonBone skeleton;
    public Bone bone;
    public SkeletonRenderer skeletonRenderer;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        rotate();
    }

    private void rotate()
    {
        var dir = rb.velocity;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}
