using UnityEngine;

public class FakeWeapon : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject realWeapon;

    public void Interact()
    {
        realWeapon.SetActive(true);
        gameObject.SetActive(false);
    }
}
