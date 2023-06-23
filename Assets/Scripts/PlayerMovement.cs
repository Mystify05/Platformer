using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    private float horizontal;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    [SerializeField] private LayerMask jumableGround;
    [SerializeField] private AudioSource jumpSoundEffect;

    private float oldHorizontal = 1f;
    private const float originalGravity = 3f; //not used yet
    public float OriginalGravity
    {
        get { return originalGravity; }
    }

    private bool jumping = false;
    private float jumpStart;
    private float jumpingPower = 10f;

    [SerializeField] private bool dashOn = false;
    public bool DashOn 
    {
        get { return dashOn; }
        set { dashOn = value; }
    }
    private bool canDash = true;
    private bool isDashing;
    public bool IsDashing { get { return isDashing; } }
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private Vector2 dashingDirection;

    public bool Flying = false;
    // Start is called before the first frame update
    private void Start()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();

        if(jumpSoundEffect == null)
            Debug.Log("Please use a jump sound effect");
    }

    // Update is called once per frame
    private void Update()
    {
        //GetAxis ist smoth und GetAxisRaw ist -1, 0 oder 1
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal < -0.5f || horizontal > 0.5f)
            oldHorizontal = horizontal;

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            jumpSoundEffect.Play();
            jumpStart = Time.time;
            jumping = true;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Time.time - jumpStart > 0.03f && jumping)
        {
            jumping = false;
            rb.gravityScale = OriginalGravity;
        }

        if (Input.GetKey(KeyCode.W) && canDash && DashOn)
        {
            StartCoroutine(Dash());
        }

        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumableGround);
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if(horizontal > 0)
        {
            state = MovementState.run;
            sprite.flipX = false;
        }
        else if(horizontal < 0)
        {
            state = MovementState.run;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y > 0.1f)
        {
            state = MovementState.jump;
        }
        else if(rb.velocity.y < -0.1f)
        {
            state = MovementState.fall;
        }

        if(Flying && !IsDashing)
        {
            state = MovementState.fly;
        }
        animator.SetInteger("State", (int)state);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.gravityScale = 0f;
        dashingDirection = new Vector2(oldHorizontal * dashingPower, 0f);
        rb.velocity = dashingDirection;
        yield return new WaitForSeconds(dashingTime);
        if(!Flying)
        {
            rb.gravityScale = OriginalGravity;
        }
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}

public enum MovementState { idle, run, jump, fall, fly}