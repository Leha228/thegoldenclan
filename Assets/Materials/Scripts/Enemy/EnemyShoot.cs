using System;
using UnityEngine;
using Spine.Unity;
using Spine;

public class EnemyShoot : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public SkeletonData skeletonData;
    public AnimationReferenceAsset idle, attacking;
    public GameObject arrow;
    public GameObject player;
    public Transform shootPoint;

    private float launchForce;
    private bool shootToPlayer = true;

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

        if (!shootToPlayer) return;
        shootToPlayer = false;
        Invoke(nameof(shoot), random(3, 5));
    }

    private float random(int a, int b) {
        System.Random rnd = new System.Random();
        return rnd.Next(a, b);
    }

    private void shoot()
    {
        TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, attacking, false);
        animationEntry.Complete += AnimationEntry_Complete;

        shootToPlayer = true;

        Invoke("shootArrow", 0.5f);
    }

    private void shootArrow() {
        Vector2 bowPosition = transform.position;
        Vector2 playerPosition = player.transform.position;

        var dir = playerPosition - bowPosition;
        var euler = transform.eulerAngles;

        euler.z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - random(10, 30);
        transform.eulerAngles = euler;

        GameObject newArrow = Instantiate(arrow, shootPoint.position, transform.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }

    private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
    {
        skeletonAnimation.state.SetAnimation(0, idle, true);
    }

}
