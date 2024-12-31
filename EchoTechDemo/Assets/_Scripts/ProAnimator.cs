using UnityEngine;

public class ProAnimator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private AnimationDataObject currentAnimation;
    private float elapsedTime;
    private float speed = 0;
    private int pointer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (currentAnimation == null) {
            return;
        }
        
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= speed) {
            UpdateAnimation();
            elapsedTime = 0;
        }
    }

    public void SetAnimation(AnimationDataObject targetAnimation) {
        if (currentAnimation == targetAnimation) {
            return;
        }

        currentAnimation = targetAnimation;
        speed = 1 / (float)currentAnimation.FramesPerSecond;
        elapsedTime = 0;
        spriteRenderer.sprite = currentAnimation.Frames[0];
        pointer = 0;
    }

    public void UpdateAnimation() {
        pointer++;
        if (pointer == currentAnimation.Frames.Length) {
            pointer = 0;
        }

        spriteRenderer.sprite = currentAnimation.Frames[pointer];
    }
}
