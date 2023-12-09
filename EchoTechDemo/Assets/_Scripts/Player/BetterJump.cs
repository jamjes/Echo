using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float fallClamp = -18;

    Rigidbody2D rb;

    public bool ApplyVariableHeight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (ApplyVariableHeight)
        {
            UpdateGravity();
        }
    }

    private void UpdateGravity()
    {
        if (rb.velocity.y < 0)
        {
            Vector2 fallVelocity = Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            rb.velocity += new Vector2(fallVelocity.x, Mathf.Clamp(fallVelocity.y, 0, fallClamp));

        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
