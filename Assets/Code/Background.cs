using UnityEngine;

public class Background : MonoBehaviour
{
    [Header("Background Tracking")]
    public Transform PlayerTransform;

    [Header("Movement Settings")]
    public float backgroundSpeedMultiplier = 0.8f;  
    public float respawnOffsetX = 5f;               
    public float deleteOffsetX = 20f;             
    public float backgroundWidth = 20f; 

    private Vector3 startPos;
    private Camera mainCamera;
    private bool isActive = true;

    public bool isTracking = false;
    public static bool isDead;

    void Start()
    {
        startPos = transform.position;

        mainCamera = Camera.main;

        if (backgroundWidth <= 0 && TryGetComponent<SpriteRenderer>(out SpriteRenderer sr))
        {
            backgroundWidth = sr.bounds.size.x;
        }
    }

    void Update()
    {
        if (PlayerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerTransform = player.transform;
            }
        }

        if (isActive && PlayerTransform != null)
        {
            Vector3 newPos = transform.position;
            newPos.x = PlayerTransform.position.x * backgroundSpeedMultiplier;
            transform.position = newPos;

            float playerX = PlayerTransform.position.x;
            float bgX = transform.position.x;

            if (bgX < playerX - deleteOffsetX)
            {
                Destroy(gameObject);
                return;
            }

            if (bgX < playerX - respawnOffsetX)
            {
                Vector3 respawnPos = startPos;
                respawnPos.x = playerX + backgroundWidth * 0.5f;
                transform.position = respawnPos;
            }
        }
    }
}
