using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _speed = 6;
    [SerializeField] private int _jumpForce = 12;
    [SerializeField] private LayerMask _groundLayer;

    public float Direction {private set; get;}
    public bool Grounded { private set; get; }

    private Rigidbody2D _rigidBody;
    private BoxCollider2D _boxCollider;
    private RewindHandler _rewindHandler;

    private bool _canMove = true;
    private bool _isRewinding = false;

    private void OnEnable()
    {
        RewindController.OnEnterRewind += StopMovement;
        RewindController.OnExitRewind += StartMovement;
    }

    private void OnDisable()
    {
        RewindController.OnEnterRewind -= StopMovement;
        RewindController.OnExitRewind -= StartMovement;
    }


    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _rewindHandler = GetComponent<RewindHandler>();
    }

    private void Update()
    {
        Grounded = IsGrounded();
        Direction = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && Grounded)
        {
            if (_canMove) Jump();
        }
    }

    private void StopMovement()
    {
        _isRewinding = true;
        _rewindHandler.StopRecording();
        _rigidBody.gravityScale = 0;
        _rigidBody.velocity = Vector2.zero;
        _canMove = false;
    }

    private void StartMovement()
    {
        _isRewinding = false;
        _rewindHandler.StartRecording();
        _rigidBody.gravityScale = 5;
        _canMove = true;
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            Move();
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.down, .3f, _groundLayer);
        return hit.collider != null;
    }

    private void Move()
    {
        _rigidBody.velocity = new Vector2(_speed * Direction, _rigidBody.velocity.y);
    }

    private void Jump()
    {
        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);
    }

    public void SetCanMove(bool value)
    {
        _canMove = value;
    }

    public void SetIsRewinding(bool value)
    {
        _canMove = value;
    }

    public bool GetCanMove()
    {
        return _canMove;
    }

    public bool GetIsRewinding()
    {
        return _isRewinding;
    }

}
