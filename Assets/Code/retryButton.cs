using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class retryButton : MonoBehaviour
{
    public Button retry;

    void Start()
    {
       retry.onClick.AddListener(() =>
           {
               Restart2Killbox.isDead = false;
               Time.timeScale = 1;
               SceneManager.LoadScene("SampleScene");
           }); 
    }
    
}
