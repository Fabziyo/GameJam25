using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnEnable : MonoBehaviour
{
    public GameObject objectToSelect;
   
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(objectToSelect);
    }
}