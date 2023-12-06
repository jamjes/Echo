using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindController : MonoBehaviour
{
    public delegate void Rewind();
    public static event Rewind OnEnterRewind;
    public static event Rewind OnExitRewind;
    private RewindHandler _handler;

    private void Start()
    {
        _handler = GetComponent<RewindHandler>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Rewind"))
        {
            OnEnterRewind();
        }
        else if (Input.GetButtonUp("Rewind") ||
            _handler.CurrentState == RewindHandler.RewindState.Retrieve && _handler._rewindablePoints.Count == 0)
        {
            OnExitRewind();
        }
    }
}
