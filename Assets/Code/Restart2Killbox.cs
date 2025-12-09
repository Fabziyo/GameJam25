using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart2Killbox : MonoBehaviour
{
    public GameObject LeaderboardUi;
    public GameObject LeaderboardManager;

    [Header("Killbox Tracking")]
    public GameObject Killbox; 
    public Transform PlayerTransform; 

    public bool isTracking = false;

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
            Time.timeScale = 0f;
            LeaderboardUi.SetActive(true);
            LeaderboardManager.SetActive(true);
        }
    }
}
