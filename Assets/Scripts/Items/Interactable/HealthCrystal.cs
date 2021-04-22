using UnityEngine;
using UnityEngine.Audio;

public class HealthCrystal : MonoBehaviour, IInteractable
{
    #pragma warning disable 0649
    [SerializeField][Range(1, 15)] private int healAmount = 10;
    [SerializeField] private AudioMixerGroup mixerGroup;
    [SerializeField] private AudioClip pickUpSound;
    #pragma warning disable 0649

    public delegate void CrystalPickedUp(int healAmount);
    public static event CrystalPickedUp IsPickedUp;

    public void Interact()
    {
        gameObject.SetActive(false);
        AudioPlayer.PlayAudio(pickUpSound, transform.position, mixerGroup);
        if (IsPickedUp != null)
            IsPickedUp(healAmount);
    }
}
