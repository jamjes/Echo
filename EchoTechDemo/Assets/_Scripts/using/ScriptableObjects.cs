using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Rewind Object")]
public class RewindDataObject : ScriptableObject {
    public Vector2 Position;
    public Quaternion Rotation;
    public Sprite Frame;
}
