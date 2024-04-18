using UnityEngine;

[CreateAssetMenu(fileName = "new Animation", menuName = "ScriptableObjects/CustomAnimation", order = 0)]
public class CustomAnimation : ScriptableObject
{
    public int AnimationSpeed;
    public Sprite[] AnimationFrames;
    public Sprite[] ReverseAnimationFrames;
    public bool Loop;
}
