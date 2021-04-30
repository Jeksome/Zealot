using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FallOnHay : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] private AudioClip impactSound;
    [SerializeField] private AudioMixerGroup mixerGroup;
    [SerializeField] private List<Enemy> enemiesAround = new List<Enemy>();
    #pragma warning restore 0649

    private bool isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCharacter>() && !isTriggered)
        {
            isTriggered = true;

            AudioPlayer.PlayAudio(impactSound, gameObject.transform.position, mixerGroup);
            other.gameObject.GetComponent<PlayerCharacter>().GetHurt(5);

            foreach (Enemy enemy in enemiesAround)
            {
                enemy.GetAgressive();
            }
        }
    }
}
