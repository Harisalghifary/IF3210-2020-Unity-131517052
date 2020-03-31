using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    Object bulletRef;

    bool isGrounded;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    Transform groundCheckL;
    [SerializeField]
    Transform groundCheckR;
    [SerializeField]
    private float runSpeed = 5f;
    [SerializeField]
    private float jumpSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bulletRef = Resources.Load("Bullet");

    }

    public void FixedUpdate()
    {
        string queuedAnim = "Player_idle";
        isGrounded = false;

        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            if (rb2d.velocity.y <= 1)
            {
                isGrounded = true;
            }
        }

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);
            queuedAnim = "Player_run";
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);
            queuedAnim = "Player_run";
            spriteRenderer.flipX = true;
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        if (!isGrounded)
        {
            queuedAnim = "Player_jump";
        }

        if (Input.GetKey("space") && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
        }

        if (Input.GetButtonDown("Fire1") && isGrounded)
        {
            animator.Play("Player_shoot");
            GameObject bullet = (GameObject)Instantiate(bulletRef);
            bullet.transform.position = new Vector3(transform.position.x + 3, transform.position.y + 1.8f, -1);
            spriteRenderer.flipX = true;
        }
        animator.Play(queuedAnim);
    }
}
