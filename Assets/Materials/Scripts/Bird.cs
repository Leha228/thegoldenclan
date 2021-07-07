
using UnityEngine;
using Spine.Unity;
using Spine;

public class Bird : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public SkeletonData skeletonData;
    public AnimationReferenceAsset idle, attacking;

    public float speed = 5;
    public float positionY;
    private float _destroyPoint;
    private bool _shootBool = true;

    public Transform rockPoint;
    public GameObject rock;

    private void Start() {
        positionY = transform.position.y + 5f;
        _destroyPoint = GameObject.Find("destroyBird").transform.position.x;
    }

    void Update() {
        if (Mathf.Round(transform.position.x) == Mathf.Round(_destroyPoint)) Destroy(this.gameObject);
        if (Mathf.Round(transform.position.x) == Mathf.Round(PlayerController.singleton.transform.position.x) && _shootBool) Shoot();
        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, positionY);      
    }

    private void Shoot() {
        TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, attacking, false);
        animationEntry.Complete += AnimationEntry_Complete;

        Instantiate(rock, rockPoint.position, rockPoint.rotation);
        _shootBool = false;
    }

    private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
    {
        skeletonAnimation.state.SetAnimation(0, idle, true);
    }
}
