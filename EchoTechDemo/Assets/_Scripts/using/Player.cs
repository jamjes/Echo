using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    [Header("Dependencies")]
    private Rigidbody2D rb2d;
    private Collider2D coll;
    private PlayerControls inputActions;
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

    [Header("Input")]
    public Vector2 InputDirection { get; private set; }
    public bool Grounded { get; private set; }
    public bool IsRewinding { get; private set; }

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        inputActions = new PlayerControls();
    }

    private void OnEnable() {
        inputActions.Enable();
        inputActions.Default.Jump.canceled += JumpCanceled;
        inputActions.Default.Rewind.performed += RewindStarted;
        inputActions.Default.Rewind.canceled += RewindCanceled;
    }

    private void OnDisable() {
        inputActions.Default.Jump.canceled -= JumpCanceled;
        inputActions.Default.Rewind.performed -= RewindStarted;
        inputActions.Default.Rewind.canceled -= RewindCanceled;
        inputActions.Disable();
    }

    private void Update() {
        if (!IsRewinding) {
            if (animator.enabled == false) {
                animator.enabled = true;
                rb2d.gravityScale = 1;
            }
            
            InputDirection = inputActions.Default.Move.ReadValue<Vector2>();
            Grounded = IsGrounded();
            ListenForInputs();

            if (Grounded) {
                if (InputDirection.x != 0) {
                    animator.CrossFade(RunAnimation, 0, 0);
                }
                else if (InputDirection.x == 0) {
                    animator.CrossFade(IdleAnimation, 0, 0);
                }
            } else if (!Grounded) {
                if (rb2d.linearVelocity.y > 0) {
                    animator.CrossFade(JumpAnimation, 0, 0);
                }
                else if (rb2d.linearVelocity.y < 0) {
                    animator.CrossFade(FallAnimation, 0, 0);
                }
            }
        }
        else {
            if (animator.enabled == true) {
                animator.enabled = false;
                rb2d.gravityScale = 0;
            }
        }
    }

    private void ListenForInputs() {
        if (direction.x > 0 && direction.x != 1) {
            direction = new Vector2(1, direction.y);
        }
        else if (direction.x < 0 && direction.x != -1) {
            direction = new Vector2(-1, direction.y);
        }

        this.direction = InputDirection;

        if (this.direction.x > 0 && facingRight == false) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            facingRight = true;
        }

        if (this.direction.x < 0 && facingRight == true) {
            transform.rotation = Quaternion.Euler(0, -180, 0);
            facingRight = false;
        }
        if (inputActions.Default.Jump.triggered && Grounded) {
            Jump();
        }
    }

    private void FixedUpdate() {
        if (IsRewinding == true) {
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

    private void RewindStarted(InputAction.CallbackContext c) {
        IsRewinding = true;
    }

    private void RewindCanceled(InputAction.CallbackContext c) {
        IsRewinding = false;
    }

    private bool IsGrounded() {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, .2f, groundLayer);
        return hit.collider != null;
    }
}
