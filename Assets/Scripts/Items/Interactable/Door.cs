using UnityEngine;
using UnityEngine.Audio;

public class Door : Openable
{
    #pragma warning disable 0649
    [SerializeField] private AudioMixerGroup mixerGroup;
    [SerializeField] private AudioClip doorSound;
    #pragma warning restore 0649

    void Start()
    {
        itemAnimator = GetComponent<Animator>();
        animationPlayingTimer = 1;       
    }

    public override void Interact()
    {
        if (!animationIsPlaying && !isOpen)
        {
            Open();
            AudioPlayer.PlayAudio(doorSound, transform.position, mixerGroup);           
        }
        else if (!animationIsPlaying && isOpen)
        {
            Close();
            AudioPlayer.PlayAudio(doorSound, transform.position, mixerGroup);
        }
    }
}
