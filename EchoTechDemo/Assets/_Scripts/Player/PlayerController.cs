using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _jumpForce = 20.5f;
    [SerializeField] private LayerMask _groundLayer;

    public PlayerStateMachine StateMachine;

    public float Direction {private set; get;}
    public bool Grounded { private set; get; }

    public Rigidbody2D RigidBody { private set; get; }
    private BoxCollider2D _boxCollider;
    private RewindHandler _rewindHandler;

    private bool _canMove = true;
    private bool _isRewinding = false;

    public delegate void Restart();
    public static event Restart onDeath;

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
        RigidBody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _rewindHandler = GetComponent<RewindHandler>();
    }

    private void Update()
    {
        Grounded = IsGrounded();
        Direction = Input.GetAxisRaw("Horizontal");

        if (Grounded)
        {
            if (Direction != 0)
            {
                StateMachine.SetState(PlayerStateMachine.State.Run);
            }
            else
            {
                StateMachine.SetState(PlayerStateMachine.State.Idle);
            }
        }
        else
        {
            if (RigidBody.velocity.y > 0)
            {
                StateMachine.SetState(PlayerStateMachine.State.Jump);
            }
            else
            {
                StateMachine.SetState(PlayerStateMachine.State.Fall);
            }
        }

        if (Input.GetButtonDown("Jump") && Grounded)
        {
            if (_canMove) Jump();
        }

        if (Input.GetButtonDown("Rewind"))
        {
            if (_canMove) StartCoroutine(PauseMovement()); StateMachine.SetState(PlayerStateMachine.State.Transform);
        }
    }

    IEnumerator PauseMovement()
    {
        _canMove = false;
        yield return new WaitForSeconds(3f);
        _canMove = true;
    }

    private void StopMovement()
    {
        _isRewinding = true;
        _rewindHandler.StopRecording();
        RigidBody.gravityScale = 0;
        RigidBody.velocity = Vector2.zero;
        _canMove = false;
    }

    private void StartMovement()
    {
        _isRewinding = false;
        _rewindHandler.StartRecording();
        RigidBody.gravityScale = 1;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Killzone"))
        {
            onDeath();
        }
    }

    private void Move()
    {
        RigidBody.velocity = new Vector2(_speed * Direction, RigidBody.velocity.y);
    }

    private void Jump()
    {
        RigidBody.velocity = new Vector2(RigidBody.velocity.x, _jumpForce);
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
