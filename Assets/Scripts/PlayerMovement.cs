using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header ("ACTION SPEEDS")]
    [SerializeField] float walkSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 20f;
    [SerializeField] Vector2 deathKick = new Vector2(20f, 20f);

    [Header ("Objects")]
    [SerializeField] GameObject arrow; 
    [SerializeField] Transform bow;

    [Header ("See Script for more private VARS")]
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;
    bool isAlive = true;
    //GameSession arrowCount;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
        //arrowCount = GetComponent<GameSession>();
    }

    void Update()
    {
        if (!isAlive) { return; }
        RunPlayer();
        ClimbLadder();
        Die();
    }

    void Die()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Spikes")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            rb.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        if (FindObjectOfType<GameSession>().totalArrows > 0)
        {
            Instantiate(arrow, bow.position, transform.rotation);
            FindObjectOfType<GameSession>().RemoveArrows();
        }
    }

    // PLAYER INPUT (X,Y) TO CHANGE VELOCITY.X
    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    // JUMP IF NOT TOUCHING GROUND LAYER
    void OnJump(InputValue value)
    {
        if ( !isAlive) {return; }
        // IF PLAYER ISNT TOUCHING GROUND, LEAVE METHOD
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return;}

        // OTHERWISE JUMP
        if(value.isPressed)
        {
            rb.velocity += new Vector2 (0f, jumpSpeed);
        }
    }

    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2 (rb.velocity.x, moveInput.y * climbSpeed);
        rb.velocity = climbVelocity;
        rb.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    void RunPlayer()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon; // IF RB.VEL.X > 0
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed); // When true activate RUNNING ANIMATION

        // FLIPS THE SPRITE
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    /*
    // FLIP -X of SPRITE PLAYER IF THEY GO LEFT
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon; // Epsilon is a cleaner way of saying 0
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }

        Debug.Log($"horiztonal speed is greater that ? {playerHasHorizontalSpeed}");
    } */
}
