using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Restart2Killbox : MonoBehaviour
{
    [Header("GameoverUI")]
    public GameObject LeaderboardUi;
    public GameObject LeaderboardManager;
    public GameObject IngameUI;
    public GameObject PauseScreen;
    public PlayerInput input;

    [Header("Killbox Tracking")]
    public GameObject Killbox; 
    public Transform PlayerTransform; 

    public bool isTracking = false;
    public static bool isDead;

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

        
        if (Killbox != null && PlayerTransform != null)
        {
            Vector3 killboxPos = Killbox.transform.position;
            killboxPos.x = PlayerTransform.position.x;
            Killbox.transform.position = killboxPos;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Killbox"))
        {
            Cursor.visible = true;
            Time.timeScale = 0f;
            isDead = true;
            input.SwitchCurrentActionMap("UI");
            LeaderboardUi.SetActive(true);
            LeaderboardManager.SetActive(true);
            IngameUI.SetActive(false);
            PauseScreen.SetActive(false);
        }
    }
}
