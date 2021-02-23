using UnityEngine;

public class Armor : MonoBehaviour, IInteractable
{
    private int armorAmount = 5;

    public delegate void ArmorPickUp(int armorAmount);
    public static event ArmorPickUp isPickedUp;

    public void Interact()
    {
        gameObject.SetActive(false);

        if (isPickedUp != null)
            isPickedUp(armorAmount);
    }

}
