using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart2Killbox : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Killbox"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
