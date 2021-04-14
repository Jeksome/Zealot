using UnityEngine;

public class MagicTorch : MonoBehaviour, IInteractable
{
    #pragma warning disable 0649
    [SerializeField] private GameObject particleEffect;
    [SerializeField] private GameObject torchLight;
    #pragma warning restore 0649

    public void Interact()
    {
            particleEffect.SetActive(true);
            torchLight.SetActive(true);      
    }
}
