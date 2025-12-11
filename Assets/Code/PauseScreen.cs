using System.Collections;
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
        input.actions.FindActionMap("Player").FindAction("Pause").performed -= TogglePause;
        input.actions.FindActionMap("UI").FindAction("Pause").performed -= TogglePause;
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
        if (Time.timeScale == 0f)
            OnUnpause();
        else
            OnPause();
    }
    void OnPause()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        //input.SwitchCurrentActionMap("UI");
        StartCoroutine(SwitchToUI());
        panel.SetActive(true);
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(false);
    }
    IEnumerator SwitchToUI()
    {
        yield return null; // 1 Frame warten
        input.SwitchCurrentActionMap("UI");
    }

    void OnUnpause()
    {
        Time.timeScale = 1f;
        StartCoroutine(SwitchToPlayer());
        //input.SwitchCurrentActionMap("Player");
        Cursor.visible = false;
        panel.SetActive(false);
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(false);
    }
    
    IEnumerator SwitchToPlayer()
    {
        yield return null;
        input.SwitchCurrentActionMap("Player");
    }
}
