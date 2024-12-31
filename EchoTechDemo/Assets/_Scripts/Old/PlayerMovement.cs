using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    [Header("Dependencies")]
    private PlayerControls inputActions;
    private Rigidbody2D rb2d;
    private Collider2D coll;
    private RewindHandler rewindHandler;
    public GameObject GFX;
    private SpriteRenderer sprRenderer;
    [SerializeField] private Animator animator;

    private static readonly int
        IdleAnimation = Animator.StringToHash("player-idle"),
        RunAnimation = Animator.StringToHash("player-run"),
        JumpAnimation = Animator.StringToHash("player-jump"),
        FallAnimation = Animator.StringToHash("player-fall");
    
    [Header("Physics")]
    private Vector2 direction;
    private float speed = 9;
    private float jumpForce = 13.5f;

    [Header("Collision")]
    private bool facingRight = true;
    [SerializeField] private LayerMask groundLayer;

    [Header("States")]
    private bool isRewinding = false;

    private void Awake() {
        inputActions = new PlayerControls();
        rb2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        rewindHandler = GetComponent<RewindHandler>();
        sprRenderer = GFX.GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        inputActions.Enable();
        inputActions.Default.Jump.canceled += JumpCanceled;
    }

    private void OnDisable() {
        inputActions.Default.Jump.canceled -= JumpCanceled;
        inputActions.Disable();
    }

    private void Update() {
        isRewinding = Input.GetKey(KeyCode.K);

        if (!isRewinding) {
            rb2d.gravityScale = 1;
            Vector2 inputDirection = inputActions.Default.Move.ReadValue<Vector2>();
            bool grounded = IsGrounded();
            ListenForInputs(grounded, inputDirection);

            if (grounded) {
                if (inputDirection.x != 0) {
                    animator.CrossFade(RunAnimation, 0, 0);
                }
                else if (inputDirection.x == 0) {
                    animator.CrossFade(IdleAnimation, 0, 0);
                }
            } else if (!grounded) {
                if (rb2d.linearVelocity.y > 0) {
                    animator.CrossFade(JumpAnimation, 0, 0);
                }
                else if (rb2d.linearVelocity.y < 0) {
                    animator.CrossFade(FallAnimation, 0, 0);
                }
            }
        }
        else {
            rb2d.gravityScale = 0;
        }
    }

    private void ListenForInputs(bool grounded, Vector2 direction) {
        if (direction.x > 0 && direction.x != 1) {
            direction = new Vector2(1, direction.y);
        }
        else if (direction.x < 0 && direction.x != -1) {
            direction = new Vector2(-1, direction.y);
        }

        this.direction = direction;

        if (this.direction.x > 0 && facingRight == false) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            facingRight = true;
        }

        if (this.direction.x < 0 && facingRight == true) {
            transform.rotation = Quaternion.Euler(0, -180, 0);
            facingRight = false;
        }
        if (inputActions.Default.Jump.triggered && grounded) {
            Jump();
        }
    }

    private void FixedUpdate() {
        if (isRewinding == true) {
            return;
        }

        rb2d.linearVelocityX = direction.x * speed;
    }

    private void Jump() {
        rb2d.linearVelocityY = jumpForce;
    }

    private void JumpCanceled(InputAction.CallbackContext c) {
        if (rb2d.linearVelocityY < 0) {
            return;
        }
        
        rb2d.linearVelocityY = 0;
    }

    private bool IsGrounded() {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, .2f, groundLayer);
        return hit.collider != null;
    }
}
