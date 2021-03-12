using UnityEngine;

public class MagicTorch : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject particleEffect;
    [SerializeField] private GameObject torchLight;
    [SerializeField] private GameObject staff; 
    public void Interact()
    {
            particleEffect.SetActive(true);
            torchLight.SetActive(true);      
    }
}
