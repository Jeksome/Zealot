using UnityEngine;

public class SecretSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject secretPassage;
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
