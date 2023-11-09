using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 6;
    public int jumpForce = 12;
    public float direction = 0;
    public Rigidbody2D rigidBody;
    public BoxCollider2D boxCollider;
    public LayerMask groundLayer;
    public bool grounded;
    public bool canMove = true;

    private void Update()
    {
        grounded = IsGrounded();
        direction = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            if (canMove) Jump();
        }

        if (Input.GetButtonDown("Rewind"))
        {
            rigidBody.gravityScale = 0;
            rigidBody.velocity = Vector2.zero;
            canMove = false;
        }

        if (Input.GetButtonUp("Rewind"))
        {
            rigidBody.gravityScale = 1;
            canMove = true;
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Move();
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .3f, groundLayer);

        return hit.collider != null;
    }

    private void Move()
    {
        rigidBody.velocity = new Vector2(speed * direction, rigidBody.velocity.y);
    }

    private void Jump()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
    }

}
