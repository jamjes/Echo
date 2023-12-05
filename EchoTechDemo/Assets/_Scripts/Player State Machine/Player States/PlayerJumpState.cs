using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerStateMachine stateMachine, Player playerRef) : base(stateMachine, playerRef)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        PlayerRef.Rb2d.velocity = new Vector2(PlayerRef.Rb2d.velocity.x, PlayerRef.JumpForce);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        
    }
}
