using UnityEngine;

public class HealthCrystal : MonoBehaviour, IInteractable
{
    private int healAmount = 10;

    public delegate void CrystalPickedUp(int healAmount);
    public static event CrystalPickedUp IsPickedUp;

    public void Interact()
    {
        gameObject.SetActive(false);

        if (IsPickedUp != null)
            IsPickedUp(healAmount);
    }

}
