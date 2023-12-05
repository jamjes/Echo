using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(PlayerStateMachine stateMachine, Player playerRef) : base(stateMachine, playerRef)
    {

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();

        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        PlayerRef.Rb2d.velocity = new Vector2(PlayerRef.Direction * PlayerRef.GroundSpeed, PlayerRef.Rb2d.velocity.y);
    }
}
