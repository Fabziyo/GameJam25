using FMODUnity;
using TMPro;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Input Actions Asset")]
    public InputActionAsset inputActions;

    public InputAction moveAction;
    public InputAction jumpAction;
    public InputAction interactAction;
    public InputAction attackAction;
    public InputAction pausePlayerAction;
    public InputAction pauseUIAction;
    public InputAction sinkAction;


    [Header("Player")]

    public Animator animator;
    public Rigidbody2D rb;

    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float groundCheckRadius = 0.1f;
    public float crouchHeight = 1f;

    [Header("Attack")]
    public Transform attackPoint; 
    public float attackRange = 1.5f;
    public LayerMask enemyLayers;
    public string enemyTag = "Enemy";

    [Header("Jump")]
    public bool isGrounded;
    public bool canJump;
    public bool wasGrounded;
    public Transform groundCheck;

    public bool isCrouching;
    public float originalHeight;

    public float moveInput;

    public LayerMask whatIsGround;
    public BoxCollider2D bodyCollider;

    public int maxJumps = 1;
    private int jumpsRemaining;

    [Header("Fast Fall")]
    public float normalGravityScale = 1f;
    public float sinkGravityScale = 3f;
    private bool isSinking;

    [Header("Object")]
    public GameObject paused_Panel;
    public GameObject option_Panel;
    public CinemachineInputAxisController cinemachineController;
    public Interactable currentInteractable;

    [Header("ScoreCounter")]
    public TextMeshProUGUI playerScore;
    public static int score;
    
    [Header("Sounds")]
    public float footstepInterval = 0.4f;
    private float footstepTimer = 0f;
    
    public PlayerInput input;

    void Start()
    {
        Time.timeScale = 1f;

        originalHeight = bodyCollider.size.y;

        //Action aktivieren
        inputActions.FindActionMap("Player").Enable();
        inputActions.FindActionMap("UI").Enable();

        //Actions
        moveAction = inputActions.FindAction("Move");
        jumpAction = inputActions.FindAction("Jump");
        interactAction = inputActions.FindAction("Interact");
        attackAction = inputActions.FindAction("Attack");
        sinkAction = inputActions.FindAction("Sink");


        //Events
        jumpAction.performed += Jump;
        interactAction.started += InteractPressed;
        attackAction.performed += Attack;
        sinkAction.performed += OnSinkStarted;
        sinkAction.canceled += OnSinkEnded;

        rb.gravityScale = normalGravityScale;
        score = 0;
        input.ActivateInput();
        input.SwitchCurrentActionMap("Player");
    }

    void OnDestroy()
    {
        jumpAction.performed -= Jump;
        interactAction.started -= InteractPressed;
        attackAction.performed -= Attack;
        sinkAction.performed -= OnSinkStarted;
        sinkAction.canceled -= OnSinkEnded;
        StopAllCoroutines();
    }

    public void OnSinkStarted(InputAction.CallbackContext ctx)
    {
        isSinking = true;
        rb.gravityScale = sinkGravityScale;
    }

    public void OnSinkEnded(InputAction.CallbackContext ctx)
    {
        isSinking = false;
        rb.gravityScale = normalGravityScale;
    }

    public void Attack(InputAction.CallbackContext ctx)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        RuntimeManager.PlayOneShot("event:/SFX/SFX_Attack");

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag(enemyTag))
            {
                Destroy(enemy.gameObject);
                RuntimeManager.PlayOneShot("event:/SFX/SFX_Bacteria-Death");
                score += 30;
                playerScore.text = score.ToString();
            }
        }
        animator.SetTrigger("Attack");
    }

    public void InteractPressed(InputAction.CallbackContext ctx)
    {
        currentInteractable?.Interact();
    }

    void Update()
    {
        Vector2 movement = moveAction.ReadValue<Vector2>();
        moveInput = 1f;

        animator.SetFloat("xSpeed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("yAchse", rb.linearVelocity.y);
        animator.SetBool("isGrounded", isGrounded);
        if (isGrounded && Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0f)
            {
                RuntimeManager.PlayOneShot("event:/SFX/SFX_Footsteps");
                footstepTimer = footstepInterval;
            }
        }
        animator.SetBool("isCrouching", isCrouching);

        //Ducken
        if (UnityEngine.Input.GetKey(KeyCode.LeftControl))
        {
            if (!isCrouching)
            {
                isCrouching = true;
                bodyCollider.size = new Vector2(bodyCollider.size.x, crouchHeight);
            }
        }
        else if (isCrouching)
        {
            isCrouching = false;
            bodyCollider.size = new Vector2(bodyCollider.size.x, originalHeight);
        }
    }

    void FixedUpdate()
    {
        //Bodencheck
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (!wasGrounded && isGrounded)
        {
            jumpsRemaining = maxJumps;
        }
        wasGrounded = isGrounded;

        //Bewegung
        float speed = isCrouching ? moveSpeed * 0.7f : moveSpeed;
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (jumpsRemaining > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpsRemaining--;
            RuntimeManager.PlayOneShot("event:/SFX/SFX_Jump_OldMan");
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("counter"))
        {
            score += 20;
            playerScore.text = score.ToString();
            Debug.Log(other.name);
        }
        else
        {
            Interactable inter = other.GetComponent<Interactable>();
            if (inter != null)
                currentInteractable = inter;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        currentInteractable = null;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
#if UNITY_EDITOR
            Handles.color = Color.red;
            Handles.DrawWireDisc(attackPoint.position, Vector3.forward, attackRange);
#endif
        }
    }
    
    


}
