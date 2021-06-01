using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
using System;

public class PlayerController : MonoBehaviour
{

    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, walk, jumping;
    private string currentState;
    private string prevState;
    private string currentAnimation;

    public float speed = 3f;
    public float jumpForce = 1f;
    private float move;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = "idle";
        setCharacterState(currentState);
    }


    void Update()
    {
        run();
    }

    private void run()
    {
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
        Debug.Log(transform.localScale.x);
        if (move != 0)
        {
            if (!currentState.Equals("jump")) { setCharacterState("walk"); }
            if (move > 0)
                transform.localScale = new Vector2(0.6f, 0.6f);
            else
                transform.localScale = new Vector2(-0.6f, 0.6f);
        }
        else
        {
            if (!currentState.Equals("jump")) { setCharacterState("idle"); }
        }

        if (Input.GetButtonDown("Jump"))
            jump();
    }

    private void jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        if (!currentState.Equals("jump")) { prevState = currentState; }
        setCharacterState("jump");
    }

    public void setAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentAnimation)) { return; }
        TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
        animationEntry.TimeScale = timeScale;
        animationEntry.Complete += AnimationEntry_Complete;
        currentAnimation = animation.name;
    }

    private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
    {
        if (currentState.Equals("jump")) { setCharacterState(prevState); }
    }

    public void setCharacterState(string state)
    {
        if (state.Equals("walk"))
            setAnimation(walk, true, 1.6f);
        else if (state.Equals("jump"))
            setAnimation(jumping, false, 1f);
        else
            setAnimation(idle, true, 1f);

        currentState = state;
    }
}
