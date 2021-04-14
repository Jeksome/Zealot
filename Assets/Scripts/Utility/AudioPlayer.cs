using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] AudioSource defaultSource;
    #pragma warning restore 0649

    public static void PlayAudio(AudioClip clip, AudioSource source) => source.PlayOneShot(clip);

    public static void PlayAudio(AudioClip clip, Vector3 pos) => AudioSource.PlayClipAtPoint(clip, pos); 
}
