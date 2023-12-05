using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine PlayerSM;
    protected Player PlayerRef;

    public PlayerState(PlayerStateMachine stateMachine, Player playerRef)
    {
        PlayerSM = stateMachine;
        PlayerRef = playerRef;
    }

    public virtual void OnStateEnter()
    {

    }

    public virtual void OnStateExit()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }
}
