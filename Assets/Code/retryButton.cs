using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class retryButton : MonoBehaviour
{
    public Button retry;
    public PlayerInput input;

    void Start()
    {
       retry.onClick.AddListener(() =>
           {
               Time.timeScale = 1;
               SceneManager.LoadScene("SampleScene");
           }); 
    }
    
}
