using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public bool IsGrounded {private set; get;}
    public float Friction {private set; get;}

    private void GroundCheck(Collision2D collision)
    {
        for(int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;
            IsGrounded = normal.y >= 0.9f;
        }
    }

    private void RetrieveFriction(Collision2D collision)
    {
        PhysicsMaterial2D material = collision.rigidbody.sharedMaterial;
        Friction = 0;

        if (material != null)
        {
            Friction = material.friction;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GroundCheck(collision);
        RetrieveFriction(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GroundCheck(collision);
        RetrieveFriction(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsGrounded = false;
        Friction = 0;
    }
}
