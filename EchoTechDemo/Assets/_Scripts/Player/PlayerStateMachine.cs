using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public enum State
    {
        Idle, Run, Jump, Fall, Transform
    };

    public State CurrentState;

    public void SetState(State targetState)
    {
        if (CurrentState == targetState)
        {
            return;
        }

        CurrentState = targetState;
    }
}
