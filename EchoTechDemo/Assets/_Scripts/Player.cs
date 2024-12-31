using UnityEngine;

public class Player : MonoBehaviour {
    private PlayerInputActions actions;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private float jumpForce = 15.75f;
    [SerializeField] private LayerMask groundLayer;
    private Vector2 inputDirection;
    private float speed = 450f;
    [SerializeField] private ProAnimator animator;
    [SerializeField] AnimationDataObject idleData;
    [SerializeField] AnimationDataObject runData;
    [SerializeField] AnimationDataObject jumpData;
    [SerializeField] AnimationDataObject fallData;
    private RewindRecorder rewindRecorder;
    private bool isRewinding;
    private void Awake() {
        actions = new PlayerInputActions();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator.SetAnimation(idleData);
        rewindRecorder = GetComponent<RewindRecorder>();
    }
    private void OnEnable() {
        actions.Enable();
    }

    private void OnDisable() {
        actions.Disable();
    }

    private void Update() {
        if (actions.Game.Rewind.triggered && isRewinding == false) {
            isRewinding = true;
            inputDirection = Vector2.zero;
            rigidBody.gravityScale = 0;
            rigidBody.linearVelocity = Vector2.zero;
        }
        else if (actions.Game.Rewind.triggered && isRewinding == true) {
            isRewinding = false;
            rigidBody.gravityScale = 1;
        }

        if (isRewinding) {
            return;
        }

        bool grounded = IsGrounded();
        
        if (actions.Game.Jump.triggered == true && grounded) {
            Jump();
        }

        inputDirection = actions.Game.Move.ReadValue<Vector2>();

        if (inputDirection.x > 0 && transform.rotation != Quaternion.Euler(0,0,0)) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (inputDirection.x < 0 && transform.rotation != Quaternion.Euler(0, 180, 0)) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (grounded) {
            if (inputDirection.x != 0) {
                animator.SetAnimation(runData);
            }
            else if (inputDirection.x == 0) {
                animator.SetAnimation(idleData);
            }
        }
        else if (!grounded) {
            if (rigidBody.linearVelocityY > 0) {
                animator.SetAnimation(jumpData);
            }
            else if (rigidBody.linearVelocityY < 0) {
                animator.SetAnimation(fallData);
            }
        }
    }

    private void FixedUpdate() {
        rigidBody.linearVelocityX = speed * inputDirection.x * Time.deltaTime;
    }

    private void Jump() {
        rigidBody.linearVelocityY += jumpForce;
    }

    private bool IsGrounded() {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0f, Vector2.down, .02f, groundLayer);
        return hit.collider != null;
    }
}
