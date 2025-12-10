using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class Player : MonoBehaviour
{
    [Header("Input Actions Asset")]
    public InputActionAsset inputActions;

    public InputAction moveAction;
    public InputAction jumpAction;
    public InputAction interactAction;
    public InputAction pausePlayerAction;
    public InputAction pauseUIAction;

    [Header("Player")]
    
    public Animator animator;
    public Rigidbody2D rb;

    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float groundCheckRadius = 0.1f;
    public float crouchHeight = 1f;

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

    [Header("Object")]
    public GameObject paused_Panel;
    public GameObject option_Panel;
    public CinemachineInputAxisController cinemachineController;
    public Interactable currentInteractable;
    
    [Header("ScoreCounter")]
    public TextMeshProUGUI playerScore;
    public static int score;

    //[Header("Audio")]
    //public AudioMixerGroup SFX;
    //public AudioSource audioSource;
    //public AudioClip SFX_Jump_OldMan_003;
    

    void Start()
    {
        Time.timeScale = 1f;

        //audioSource = GetComponent<AudioSource>();
        //if (SFX_Jump_OldMan_003 != null)
            //audioSource.PlayOneShot(SFX_Jump_OldMan_003, 0.7f);

        originalHeight = bodyCollider.size.y;

        //Action aktivieren
        inputActions.FindActionMap("Player").Enable();
        inputActions.FindActionMap("UI").Enable();

        //Actions
        moveAction = inputActions.FindAction("Move");
        jumpAction = inputActions.FindAction("Jump");
        interactAction = inputActions.FindAction("Interact");

        //pausePlayerAction = inputActions.FindActionMap("Player").FindAction("Pause");
        //pauseUIAction = inputActions.FindActionMap("UI").FindAction("Pause");

        //Events
        jumpAction.performed += Jump;
        interactAction.started += InteractPressed;
        //pausePlayerAction.performed += TogglePause;
        //pauseUIAction.performed += TogglePause;
        score = 0;
    }

    /*public void TogglePause(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0f)
            OnUnpause();
        else
            OnPause();
    }

    void OnPause()
    {
        Time.timeScale = 0f;

        paused_Panel.SetActive(true);
        option_Panel.SetActive(false);

        //Player Action Map deaktivieren → UI aktiviert
        inputActions.FindActionMap("Player").Disable();
        inputActions.FindActionMap("UI").Enable();
    }

    void OnUnpause()
    {
        Time.timeScale = 1f;

        paused_Panel.SetActive(false);
        option_Panel.SetActive(false);

        //Player Actions wieder aktivieren
        inputActions.FindActionMap("UI").Enable();
        inputActions.FindActionMap("Player").Enable();

    }*/

    void OnDestroy()
    {
        jumpAction.performed -= Jump;
        interactAction.started -= InteractPressed;
        //pausePlayerAction.performed -= TogglePause;
        //pauseUIAction.performed -= TogglePause;
    }

    //Input
    public void InteractPressed(InputAction.CallbackContext ctx)
    {
        currentInteractable?.Interact();
    }

    void Update()
    {
        //Bewegung
        Vector2 movement = moveAction.ReadValue<Vector2>();
        //moveInput = movement.x;
        moveInput = 1f;

        animator.SetFloat("xSpeed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("yAchse", rb.linearVelocity.y);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isCrouching", isCrouching);

        // Ducken mit Input-Methode
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
            //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/SFX_Jump_OldMan");
            
        }
    }

    //Trigger 2D
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("counter"))
        {
            score += 5;
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


}
