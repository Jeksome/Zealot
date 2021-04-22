using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
    public static AudioSource PlayAudio(AudioClip clip, Vector3 pos, AudioMixerGroup mixerGroup)
    {
        var temporarySource = new GameObject("TemporaryAudioSource");
        temporarySource.transform.position = pos;
        var audioSource = temporarySource.AddComponent<AudioSource>();
        audioSource.volume = 0.45f;
        audioSource.priority = 70;
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = mixerGroup;
        Destroy(temporarySource, clip.length);
        audioSource.Play();
        return audioSource;
    }
}
