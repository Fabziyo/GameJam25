using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnSelectLoadScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Button button;
    public string sceneName;
    public bool setTimeNormal;
    public bool resetMusic;

    void Start()
    {
        button.onClick.AddListener(() =>
        {
            if (setTimeNormal)
            {
                Time.timeScale = 1;
            }

            if (resetMusic)
            {
                
            }
            SceneManager.LoadScene(sceneName);
        }); 
    }
}
