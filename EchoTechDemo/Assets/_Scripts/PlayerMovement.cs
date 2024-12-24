using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    private PlayerControls inputActions;
    private Vector2 direction;
    private Rigidbody2D rb2d;
    private float speed = 9;
    private float jumpForce = 13.5f;
    private bool facingRight = true;
    private Collider2D coll;
    [SerializeField] private LayerMask groundLayer;

    private void Awake() {
        inputActions = new PlayerControls();
        rb2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
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
        float inputX = inputActions.Default.Move.ReadValue<Vector2>().x;
        float inputY = inputActions.Default.Move.ReadValue<Vector2>().y;
        bool grounded = IsGrounded();

        if (inputX > 0 && inputX != 1) {
            inputX = 1;
        }
        else if (inputX < 0 && inputX != -1) {
            inputX = -1;
        }
        
        direction = new Vector2(inputX, inputY);

        if (direction.x > 0 && facingRight == false) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            facingRight = true;
        }
        
        if (direction.x < 0 && facingRight == true) {
            transform.rotation = Quaternion.Euler(0, -180, 0);
            facingRight = false;
        }

        if (inputActions.Default.Jump.triggered && grounded) {
            Jump();
        }
    }

    private void FixedUpdate() {
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
