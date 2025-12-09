using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public GameObject LeaderboardUi;
    public GameObject LeaderboardManager;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Time.timeScale = 0f;
            LeaderboardUi.SetActive(true);
            LeaderboardManager.SetActive(true);
        }
    }


}
