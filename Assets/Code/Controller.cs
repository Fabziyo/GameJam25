using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [Header("Input Actions Asset")]
    public InputActionAsset inputActions;

    private InputAction moveAction;
    private InputAction jumpAction;

    [Header("Player Settings")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Input Actions
        moveAction = inputActions.FindAction("Move");
        jumpAction = inputActions.FindAction("Jump");
    }

    public void OnEnable()
    {
        if (moveAction != null) moveAction.Enable();
        if (jumpAction != null)
        {
            jumpAction.Enable();
            jumpAction.performed += Jump;
        }
    }

    public void OnDisable()
    {
        if (moveAction != null) moveAction.Disable();

        if (jumpAction != null)
        {
            jumpAction.performed -= Jump;   
            jumpAction.Disable();
        }
    }

    public void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
    }

    public void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        rb.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
    }
}
