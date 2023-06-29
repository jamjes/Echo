using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIController", menuName = "InputHandler/AIController")]
public class AIController : InputHandler
{
    public override float RetrieveMoveInput()
    {
        return 1f;
    }
    
    public override bool RetrieveJumpInput()
    {
        return true;
    }
}
