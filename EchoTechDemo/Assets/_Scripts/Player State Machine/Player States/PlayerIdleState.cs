using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerStateMachine stateMachine, Player playerRef) : base(stateMachine, playerRef)
    {

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        PlayerRef.Rb2d.velocity = Vector2.zero;
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
