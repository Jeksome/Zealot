using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] private List<AudioClip> steps = new List<AudioClip>();
    private bool footstepSoundIsPlaying;
    #pragma warning restore 0649

    private AudioSource source;
    private int random; 

    private void Start() => source = GetComponent<AudioSource>();

    public void ToggleFootstep(bool isSprinting, bool isWalkingBackwards)
    {
        if (footstepSoundIsPlaying == false)
        {
            footstepSoundIsPlaying = true;
            if (isSprinting == true)
            {
                Invoke("GenerateFootstepSound", 0.05f);
            }
            else if(isWalkingBackwards == true)
            {
                Invoke("GenerateFootstepSound", 0.4f);
            }
            else 
            {
                Invoke("GenerateFootstepSound", 0.2f);
            }            
        }        
    }   

    private void GenerateFootstepSound()
    {
        random = Random.Range(0, 10);
        source.PlayOneShot(steps[random]);
        footstepSoundIsPlaying = false;
    }
}
