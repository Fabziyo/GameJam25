using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{

    public Button resumeButton;
    public Button settingsButton;
    public Button mainMenuButton;
    public Button controlsButton;
    
    public GameObject panel;
    public GameObject settingsPanel;
    public GameObject controlsPanel;
    
    public PlayerInput input;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            input.actions.FindActionMap("Player").FindAction("Pause").performed -= TogglePause;
            input.actions.FindActionMap("UI").FindAction("Pause").performed -= TogglePause;
            SceneManager.LoadScene("MainMenu");
            //AudioSystem.instance.musicEmitter.EventInstance.setParameterByNameWithLabel("LevelSwitch2", "Menu");
        });
        settingsButton.onClick.AddListener(() =>
        {
            settingsPanel.SetActive(true);
            controlsPanel.SetActive(false);
            panel.SetActive(false);
        });
        controlsButton.onClick.AddListener(() =>
        {
            settingsPanel.SetActive(false);
            controlsPanel.SetActive(true);
            panel.SetActive(false);
        });
        resumeButton.onClick.AddListener(OnUnpause);

        input.actions.FindActionMap("Player").FindAction("Pause").performed += TogglePause;
        input.actions.FindActionMap("UI").FindAction("Pause").performed += TogglePause;
    }

    private void TogglePause(InputAction.CallbackContext obj)
    {
        if(Time.timeScale == 0f)
            OnUnpause();
        else
            OnPause();
    }
    void OnPause()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        input.SwitchCurrentActionMap("UI");
        panel.SetActive(true);
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(false);
    }

    void OnUnpause()
    {
        Time.timeScale = 1f;
        input.SwitchCurrentActionMap("Player");
        Cursor.visible = false;
        panel.SetActive(false);
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(false);
    }
}
