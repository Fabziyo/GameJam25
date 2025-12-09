using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart2Killbox : MonoBehaviour
{
    public GameObject LeaderboardUi;
    public GameObject LeaderboardManager;
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Killbox"))
        {
            Time.timeScale = 0f;
            LeaderboardUi.SetActive(true);
            LeaderboardManager.SetActive(true);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
