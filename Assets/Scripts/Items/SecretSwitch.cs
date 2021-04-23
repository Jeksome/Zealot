using UnityEngine;
using UnityEngine.Audio;

public class SecretSwitch : MonoBehaviour, IInteractable
{
    #pragma warning disable 0649
    [SerializeField] private GameObject secretPassage;
    [SerializeField] private AudioMixerGroup mixerGroup;
    [SerializeField] private AudioClip openingSound;
    #pragma warning restore 0649

    private Animator switchAnimator;
    private bool activated;

    public void Interact()
    {
        switchAnimator = GetComponent<Animator>();

        if (!activated)
        {
            switchAnimator.Play("pullSwitch");
            AudioPlayer.PlayAudio(openingSound, transform.position, mixerGroup);
            secretPassage.gameObject.SetActive(false);
            activated = true;
        }       
    }
}
