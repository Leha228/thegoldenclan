using UnityEngine;
using Spine.Unity;
using Spine;

public class PlayerController : MonoBehaviour
{

    public SkeletonAnimation skeletonAnimation;
    public SkeletonData skeletonData;
    public AnimationReferenceAsset idle, walk, jumping, attacking, aiming;
    private string currentState;
    private string prevState;
    private string currentAnimation;

    public float speed = 3f;
    public float jumpForce = 5f;
    private float move;

    private Rigidbody2D rb;
    private bool aimBool = false;
    private CameraController cameraController;
    public Camera cam;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = "idle";

        setCharacterState(currentState);

        cameraController = cam.GetComponent<CameraController>();
    }

    void Update()
    {
        run();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.name != "finish") return;
        
        cameraController.nextLevel();
    }

    private void run()
    {
        if (Input.GetButton("Fire2"))
        {
            aimBool = true;
            aim();
            if (Input.GetButtonDown("Fire1")) attack();
        }
        else
        {
            if (aimBool)
            {
                setCharacterState("idle");
                aimBool = false;
            }
        }

        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if (move != 0)
        {
            if (!currentState.Equals("jump") && !currentState.Equals("aim") && !currentState.Equals("aim1")) { setCharacterState("walk"); }
            if (move > 0)
                transform.localScale = new Vector2(0.5f, 0.5f);
            else
                transform.localScale = new Vector2(-0.5f, 0.5f);
        }
        else
        {
            if (!currentState.Equals("jump") && !currentState.Equals("aim") && !currentState.Equals("aim1") && !currentState.Equals("attack_aim")) { setCharacterState("idle"); }
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

    private void attack()
    {
        setCharacterState("attack_aim");
    }

    private void aim()
    {
        if (!currentState.Equals("aim")) { prevState = currentState; }
        setCharacterState("aim");
    }

    public void setAnimation(int index, AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentAnimation)) { return; }


        TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(index, animation, loop);
        animationEntry.TimeScale = timeScale;
        animationEntry.Complete += AnimationEntry_Complete;
        currentAnimation = animation.name;
    }

    public void addAnimation(AnimationReferenceAsset animation, bool loop) {
        TrackEntry animationEntry = skeletonAnimation.state.AddAnimation(1, animation, loop, 0);
        animationEntry.Complete += AnimationEntry_Complete;
    }

    private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
    {
        if (currentState.Equals("jump")) { setCharacterState(prevState); }
        if (currentState.Equals("attack_aim")) { setCharacterState("aim"); }
        //if (currentState.Equals("aim")) { setCharacterState("aim1"); }
    }

    public void setCharacterState(string state)
    {
        if (state.Equals("walk"))
            setAnimation(0, walk, true, 1.6f);
        else if (state.Equals("jump"))
            setAnimation(0, jumping, false, 0.7f);
        else if (state.Equals("aim"))
            setAnimation(0, aiming, false, 1f);
        else if (state.Equals("attack_aim"))
            setAnimation(0, attacking, false, 1f);
        else
            setAnimation(0, idle, true, 1f);

        currentState = state;
    }
}
