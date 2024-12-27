using UnityEngine;

public class AdvancedAnimator : MonoBehaviour {
    float elapsedTime;
    int index = 0;
    public AnimationDataObject runAnimation;
        public AnimationDataObject currentAnimation { get; private set; }
    SpriteRenderer sprRenderer;

    private void Start() {
        sprRenderer = GetComponent<SpriteRenderer>();
        currentAnimation = runAnimation;
        sprRenderer.sprite = currentAnimation.Frames[index];
    }

    private void Update() {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= (1 / (float)currentAnimation.FramesPerSecond)) {
            index++;
            if (index >= currentAnimation.Frames.Length) {
                if (currentAnimation.Loop == true) {
                    index = 0;
                }
                else if (currentAnimation.Loop == false) {
                    return;
                }
            }
            sprRenderer.sprite = currentAnimation.Frames[index];
            elapsedTime = 0;
        }
    }
}
