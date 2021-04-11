using UnityEngine;

public class FakeWeapon : MonoBehaviour, IInteractable
{
    #pragma warning disable 0649
    [SerializeField] private GameObject realWeapon;
    #pragma warning restore 0649

    public void Interact()
    {
        realWeapon.SetActive(true);
        gameObject.SetActive(false);
    }
}
