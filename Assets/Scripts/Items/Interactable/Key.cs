using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public bool IsPickedUp { get { return isPickedUp; } }
    private bool isPickedUp;

    public void Interact()
    {
        isPickedUp = true;
        gameObject.SetActive(false);        
    }
}
