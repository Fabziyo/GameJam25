using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectOnBack : MonoBehaviour
{
    public GameObject objectToSelect;
    public Button back;
    public GameObject menu;
    public GameObject startMenu;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        back.onClick.AddListener(() =>
        {
            menu.SetActive(false);
            startMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(objectToSelect);
        });
    }
}