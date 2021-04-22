using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PlayerAudio : MonoBehaviour

{
    #pragma warning disable 0649
    [SerializeField] private List<AudioClip> steps = new List<AudioClip>();
    #pragma warning restore 0649

    private AudioSource source;
    private bool footstepSoundIsPlaying;
    private float footstepDelay;

    private void Awake() => source = GetComponent<AudioSource>();

    public void ToggleFootstep(bool isSprinting, bool isWalkingBackwards)
    {
        if (!footstepSoundIsPlaying)
        {
            footstepSoundIsPlaying = true;
            if (isSprinting) 
            {
                footstepDelay = 0.05f;
                StartCoroutine(GenerateFootstepSound(footstepDelay));
            }
            else if (isWalkingBackwards)
            {
                footstepDelay = 0.4f;
                StartCoroutine(GenerateFootstepSound(footstepDelay));
            }
            else
            {
                footstepDelay = 0.2f;
                StartCoroutine(GenerateFootstepSound(footstepDelay));
            }
        }
    }

    private IEnumerator GenerateFootstepSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        int random = Random.Range(0, 10);
        source.PlayOneShot(steps[random]);
        footstepSoundIsPlaying = false;        
    }
}