using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindController : MonoBehaviour
{
    public delegate void Rewind();
    public static event Rewind OnEnterRewind;
    public static event Rewind OnExitRewind;

    private void Update()
    {
        if (Input.GetButtonDown("Rewind"))
        {
            OnEnterRewind();
        }
        
        if (Input.GetButtonUp("Rewind"))
        {
            OnExitRewind();
        }
    }
}
