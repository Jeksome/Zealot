using UnityEngine;
using UnityEngine.Audio;

public class LockedDoor : Openable, IOpenable
{
    #pragma warning disable 0649
    [SerializeField] private Key key;
    [SerializeField] private AudioMixerGroup mixerGroup;
    [SerializeField] private AudioClip doorSound;
    #pragma warning restore 0649

    void Start()
    {
        itemAnimator = GetComponent<Animator>();
        animationPlayingTimer = 2.3f;
    }

    public override void Interact()
    {
        if (key.IsPickedUp == true && !animationIsPlaying && isOpen == false)
        {
            Open();
            AudioPlayer.PlayAudio(doorSound, transform.position, mixerGroup);
        }
        else if (!animationIsPlaying && isOpen == true)
        {
            Close();
            AudioPlayer.PlayAudio(doorSound, transform.position, mixerGroup);
        }           
    } 
}
