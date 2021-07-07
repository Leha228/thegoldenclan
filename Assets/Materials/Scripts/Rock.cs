using UnityEngine;
using Spine.Unity;
using Spine;

public class Rock : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public SkeletonData skeletonData;
    public AnimationReferenceAsset remove;

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.name == "bird(Clone)") return;
        skeletonAnimation.state.SetAnimation(0, remove, false);
        Invoke("Destroy", 0.5f);
    }
    
    private void Destroy() {
        Destroy(this.gameObject);
    }
}
