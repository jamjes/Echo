using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide : MonoBehaviour
{
    public bool Walled;
    public BoxCollider2D _boxCollider;
    public LayerMask _wallLayer;
    public PlayerController player;
    private Vector2 _detectDirection;
    [SerializeField] private float _slideSpeed = 3f;
    public Rigidbody2D rb;
    
    private void Update()
    {
        if (player.Direction == 0) _detectDirection = Vector2.zero;
        else
        {
            if (player.Direction > 0) _detectDirection = Vector2.right;
            else if (player.Direction < 0) _detectDirection = Vector2.left;
        }

        Walled = IsWalled();

        if (Walled && (player.Direction != 0 && rb.velocity.y < 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -(_slideSpeed), 0));
        }
    }

    private bool IsWalled()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, _detectDirection, .3f, _wallLayer);
        return hit.collider != null;
    }
}
