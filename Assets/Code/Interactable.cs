using UnityEngine;

public class Interactable : MonoBehaviour
{
    public event System.Action onInteract;

    //virtual funktion darf erweitert/überschrieben werden
    public virtual void Interact()
    {
        Debug.Log("Interacted with " + name);
        if (onInteract != null)
            onInteract.Invoke();

    }
}
