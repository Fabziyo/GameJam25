using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public Button startButton;
    public Button settingsButton;
    public Button creditsButton;
    public Button quitButton;
    public Button controlButton;

    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject controlsMenu;
    public GameObject StartMenu;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
        startButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("SampleScene");
            //AudioSystem.instance.musicEmitter.EventInstance.setParameterByNameWithLabel("LevelSwitch2", "Ingame-1");   FMOD level music switch
        });
        
        settingsButton.onClick.AddListener(() =>
        {
            settingsMenu.SetActive(true);
            creditsMenu.SetActive(false);
            controlsMenu.SetActive(false);
            StartMenu.SetActive(false);
        });
        creditsButton.onClick.AddListener(() =>
        {
            creditsMenu.SetActive(true);
            settingsMenu.SetActive(false);
            controlsMenu.SetActive(false);
            StartMenu.SetActive(false);
        });
        controlButton.onClick.AddListener(() =>
        {
            controlsMenu.SetActive(true);
            creditsMenu.SetActive(false);
            settingsMenu.SetActive(false);
            StartMenu.SetActive(false);
        });
        quitButton.onClick.AddListener(() => Application.Quit());
        
        EventSystem.current.SetSelectedGameObject(startButton.gameObject);
    }
    
}
