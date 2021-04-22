using UnityEngine;
using System.Collections.Generic;

public class AudioLibrary : MonoBehaviour
{
    private List<AudioClip> soundsToPlay;
    public AudioTracks tracks; 

    [System.Serializable]
    public class AudioTracks
    {
        public string Name;
        public AudioSource source;
        public AudioObjects[] audio;
    }
    
    [System.Serializable]
    public class AudioObjects
    {
        public AudioType type;
        public AudioClip clip;
    }


    /*   public void PickAudio (Enemy enemy, string audioName, AudioSource source)
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
       }*/
}
