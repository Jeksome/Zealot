using UnityEngine;

public class HealthCrystal : MonoBehaviour, IInteractable
{
    #pragma warning disable 0649
    [SerializeField][Range(1, 15)] private int healAmount = 10;
    #pragma warning disable 0649

    public delegate void CrystalPickedUp(int healAmount);
    public static event CrystalPickedUp IsPickedUp;

    public void Interact()
    {
        gameObject.SetActive(false);

        if (IsPickedUp != null)
            IsPickedUp(healAmount);
    }
}
