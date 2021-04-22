using UnityEngine;
using UnityEngine.Audio;

public class FakeWeapon : MonoBehaviour, IInteractable
{
    #pragma warning disable 0649
    [SerializeField] private GameObject realWeapon;
    [SerializeField] private AudioMixerGroup mixerGroup;
    [SerializeField] private AudioClip pickUpSound;
#pragma warning restore 0649

    public void Interact()
    {
        realWeapon.SetActive(true);
        AudioPlayer.PlayAudio(pickUpSound, transform.position, mixerGroup);
        gameObject.SetActive(false);
    }
}
