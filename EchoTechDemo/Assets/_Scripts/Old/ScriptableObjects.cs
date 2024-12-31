using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Animation Object")]
public class AnimationDataObject : ScriptableObject {
    public Sprite[] Frames;
    public int FramesPerSecond;
    public bool Loop;
}

[CreateAssetMenu(menuName = "Scriptable Objects/Rewind Object")]
public class RewindDataObject : ScriptableObject {
    public Vector2 Position;
    public Quaternion Rotation;
}
