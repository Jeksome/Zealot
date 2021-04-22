using UnityEngine;
using UnityEngine.Audio;

public class Key : MonoBehaviour, IInteractable
{ 
    public bool IsPickedUp { get { return isPickedUp; } }
    private bool isPickedUp;

    #pragma warning disable 0649
    [SerializeField] private AudioMixerGroup mixerGroup;
    [SerializeField] private AudioClip pickUpSound;
    #pragma warning restore 0649

    public void Interact()
    {
        isPickedUp = true;
        gameObject.SetActive(false);
        AudioPlayer.PlayAudio(pickUpSound, transform.position, mixerGroup);
    }
}
