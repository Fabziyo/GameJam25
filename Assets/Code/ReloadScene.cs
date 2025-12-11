using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public PlayerInput input;
    void Start()
    {
        input.actions.FindActionMap("Player").FindAction("Retry").performed -= reloadscene;
    }
    
    private void reloadscene(InputAction.CallbackContext obj)
    {
        ReloadScene2();
    }
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene2();
        }
    }*/

    
    public void ReloadScene2()
    {
        Restart2Killbox.isDead = false;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
