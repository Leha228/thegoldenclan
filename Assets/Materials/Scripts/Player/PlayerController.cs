
using UnityEngine;
using Spine.Unity;
using Spine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController singleton { get; private set;}
    public SkeletonAnimation skeletonAnimation;
    public SkeletonData skeletonData;
    public AnimationReferenceAsset idle, walk, jumping, attacking, aiming;

    private string _currentState;
    private string _prevState;
    private string _currentAnimation;

    public float speed = 3f;
    public float jumpForce = 5f;
    private float move;

    private Rigidbody2D rb;
    private bool aimBool = false;

    private void Awake() {
        singleton = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _currentState = "idle";

        setCharacterState(_currentState);
    }

    void Update()
    {
        Run();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.name == "plot") this.transform.parent = other.transform;

        int indexLevel;
        bool res = int.TryParse(other.collider.name, out indexLevel);
        if (res == false || indexLevel == -1) return;
        CameraController.singleton.NextLevel(indexLevel + 1);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.name == "plot") this.transform.parent = null;
    }

    private void Run()
    {
        if (Input.GetButton("Fire2")) {
            aimBool = true;
            Aim();
            if (Input.GetButtonDown("Fire1")) Attack();
        } else {
            if (aimBool) { setCharacterState("idle"); aimBool = false; }
        }

        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if (move != 0) {
            if (!_currentState.Equals("jump") && !_currentState.Equals("aim") && !_currentState.Equals("aim1")) { setCharacterState("walk"); }
            if (move > 0)
                transform.localScale = new Vector2(0.5f, 0.5f);
            else
                transform.localScale = new Vector2(-0.5f, 0.5f);
        } else {
            if (!_currentState.Equals("jump") && !_currentState.Equals("aim") && !_currentState.Equals("aim1") && !_currentState.Equals("attack_aim")) { setCharacterState("idle"); }
        }

        if (Input.GetButtonDown("Jump"))
            Jump();

    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        if (!_currentState.Equals("jump")) { _prevState = _currentState; }
        setCharacterState("jump");
    }

    private void Attack()
    {
        setCharacterState("attack_aim");
    }

    private void Aim()
    {
        if (!_currentState.Equals("aim")) { _prevState = _currentState; }
        setCharacterState("aim");
    }

    public void SetAnimation(int index, AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(_currentAnimation)) { return; }


        TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(index, animation, loop);
        animationEntry.TimeScale = timeScale;
        animationEntry.Complete += AnimationEntry_Complete;
        _currentAnimation = animation.name;
    }

    public void AddAnimation(AnimationReferenceAsset animation, bool loop)
    {
        TrackEntry animationEntry = skeletonAnimation.state.AddAnimation(1, animation, loop, 0);
        animationEntry.Complete += AnimationEntry_Complete;
    }

    private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
    {
        if (_currentState.Equals("jump")) { setCharacterState(_prevState); }
        if (_currentState.Equals("attack_aim")) { setCharacterState("aim"); }
        //if (currentState.Equals("aim")) { setCharacterState("aim1"); }
    }

    public void setCharacterState(string state)
    {
        if (state.Equals("walk"))
            SetAnimation(0, walk, true, 1.6f);
        else if (state.Equals("jump"))
            SetAnimation(0, jumping, false, 0.7f);
        else if (state.Equals("aim"))
            SetAnimation(0, aiming, false, 1f);
        else if (state.Equals("attack_aim"))
            SetAnimation(0, attacking, false, 1f);
        else
            SetAnimation(0, idle, true, 1f);

        _currentState = state;
    }
}
