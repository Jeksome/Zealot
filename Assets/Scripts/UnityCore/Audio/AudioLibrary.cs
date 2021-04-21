using UnityEngine;
using System.Collections.Generic;

public class AudioLibrary : MonoBehaviour
{
    public List<AudioClip> zombieSounds;
    public List<AudioClip> antDemonSounds;

    private List<AudioClip> soundsToPlay;

    public void PickAudio (Enemy enemy, string audioName, AudioSource source)
    {
        if (enemy is Zombie)
        {
            soundsToPlay = zombieSounds;
        }
        else if (enemy is AntDemon)
        {
            soundsToPlay = antDemonSounds;
        }

        foreach (AudioClip clip in soundsToPlay)
        {
            if (clip.ToString() == audioName)
            {
                AudioPlayer.PlayAudio(clip, source);
            }               
        }
    }
}
