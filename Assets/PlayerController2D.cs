using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;

    [SerializeField]
    private float runSpeed = 5f;
    [SerializeField]
    private float jumpSpeed = 10f;

    bool isGrounded;

    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    Transform groundCheckL;
    [SerializeField]
    Transform groundCheckR;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))||
            Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")))

        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            animator.Play("Player_jump");
        }

            if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rigidBody.velocity = new Vector2(runSpeed, rigidBody.velocity.y);

            if(isGrounded)
                animator.Play("Player_run");

            spriteRenderer.flipX = false;
        }
        else if(Input.GetKey("a") || Input.GetKey("left"))
        {
            rigidBody.velocity = new Vector2(-runSpeed, rigidBody.velocity.y);

            if(isGrounded)
                animator.Play("Player_run");

            spriteRenderer.flipX = true;
        }
        else
        {
            if (isGrounded)
                animator.Play("Player_idle");

            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }

        if (Input.GetKey("space") && isGrounded)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
            animator.Play("Player_jump");
        }
    }
}
