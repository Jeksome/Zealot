using UnityEngine;
using UnityEngine.Audio;

public class Armor : MonoBehaviour, IInteractable
{
    #pragma warning disable 0649
    [SerializeField][Range(1,100)] private int armorAmount = 5;
    [SerializeField] private AudioMixerGroup mixerGroup;
    [SerializeField] private AudioClip pickUpSound;
    #pragma warning restore 0649

    public delegate void ArmorPickUp(int armorAmount);
    public static event ArmorPickUp isPickedUp;

    public void Interact()
    {
        gameObject.SetActive(false);
        AudioPlayer.PlayAudio(pickUpSound, transform.position, mixerGroup);
        
        if (isPickedUp != null) isPickedUp(armorAmount);
    }

}
