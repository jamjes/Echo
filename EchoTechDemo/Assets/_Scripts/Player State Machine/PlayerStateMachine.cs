using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrentState { private set; get; }

    public void Init(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.OnStateExit();
    }

    public void ChangeState(PlayerState newState)
    {
        CurrentState.OnStateExit();
        CurrentState = newState;
        CurrentState.OnStateEnter();
    }
}
