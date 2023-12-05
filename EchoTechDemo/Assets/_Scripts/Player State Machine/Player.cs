using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { private set; get; }
    public PlayerIdleState IdleState { private set; get; }
    public PlayerRunState RunState { private set; get; }
    public PlayerJumpState JumpState { private set; get; }

    public PlayerFallState FallState { private set; get; }
    public Rigidbody2D Rb2d { private set; get; }

    public float Direction; //{ private set; get; }
    public float GroundSpeed; //{ private set; get; }
    public float JumpForce;

    public bool IsGrounded { private set; get; }
    public BoxCollider2D BoxCol { private set; get; }
    public LayerMask GroundLayer;

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(StateMachine, this);
        RunState = new PlayerRunState(StateMachine, this);
        JumpState = new PlayerJumpState(StateMachine, this);
        FallState = new PlayerFallState(StateMachine, this);
        Rb2d = GetComponent<Rigidbody2D>();
        BoxCol = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        StateMachine.Init(IdleState);
        GroundSpeed = 10;
        JumpForce = 24;
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
        
        IsGrounded = GroundCheck();
        Direction = Input.GetAxisRaw("Horizontal");

        if (IsGrounded)
        {
            if (Direction != 0 && StateMachine.CurrentState != RunState)
            {
                StateMachine.ChangeState(RunState);
            }
            else if (Direction == 0 && StateMachine.CurrentState != IdleState)
            {
                StateMachine.ChangeState(IdleState);
            }

            if (Input.GetButtonDown("Jump"))
            {
                StateMachine.ChangeState(JumpState);
            }
        }
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private bool GroundCheck()
    {
        RaycastHit2D hit = Physics2D.BoxCast(BoxCol.bounds.center, BoxCol.bounds.size, 0f, Vector2.down, .3f, GroundLayer);
        return hit.collider != null;
    }
}
