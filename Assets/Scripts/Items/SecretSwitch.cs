using UnityEngine;

public class SecretSwitch : MonoBehaviour, IInteractable
{
    #pragma warning disable 0649
    [SerializeField] private GameObject secretPassage;
    #pragma warning restore 0649

    private Animator switchAnimator;
    private bool activated;

    public void Interact()
    {
        switchAnimator = GetComponent<Animator>();

        if (!activated)
        {
            switchAnimator.Play("pullSwitch");
            secretPassage.gameObject.SetActive(false);
            activated = true;
        }       
    }
}
