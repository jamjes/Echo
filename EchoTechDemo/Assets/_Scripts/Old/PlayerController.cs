using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb2d;
    private Collider2D coll;
    private PlayerControls inputActions;
    [SerializeField] private AdvancedAnimator animator;

    private void Awake() {
        inputActions = new PlayerControls();
        rb2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    private void OnEnable() {
        inputActions.Enable();
    }

    private void OnDisable() {
        inputActions.Disable();
    }

    private void FixedUpdate() {
        
    }
}
