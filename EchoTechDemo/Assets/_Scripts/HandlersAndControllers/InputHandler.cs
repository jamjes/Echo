using UnityEngine;

public abstract class InputHandler : ScriptableObject
{
    public abstract float RetrieveMoveInput();

    public abstract bool RetrieveJumpInput();
}
