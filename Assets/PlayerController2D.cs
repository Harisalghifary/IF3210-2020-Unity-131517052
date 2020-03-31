using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
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
        if(Input.GetKey("d") || Input.GetKey("right"))
        {
            rigidBody.velocity = new Vector2(2, rigidBody.velocity.y);
            animator.Play("Player_run");
            spriteRenderer.flipX = false;
        }
        else if(Input.GetKey("a") || Input.GetKey("left"))
        {
            rigidBody.velocity = new Vector2(-2, rigidBody.velocity.y);
            animator.Play("Player_run");
            spriteRenderer.flipX = true;
        }
        else
        {
            animator.Play("Player_idle");
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }

        if (Input.GetKey("space"))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 3);
            animator.Play("Player_jump");
        }
    }
}
