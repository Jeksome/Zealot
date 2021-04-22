using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FallOnHay : MonoBehaviour
{
    private bool isTriggered;

    #pragma warning disable 0649
    [SerializeField] private PlayerCharacter player;
    [SerializeField] private AudioClip impactSound;
    [SerializeField] private AudioMixerGroup mixerGroup;
    [SerializeField] private List<Enemy> enemiesAround = new List<Enemy>();
    #pragma warning restore 0649

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject && isTriggered == false)
        {
            isTriggered = true;
            Debug.Log("is triggered");
            AudioPlayer.PlayAudio(impactSound, gameObject.transform.position, mixerGroup);
            player.GetHurt(5);
            foreach (Enemy enemy in enemiesAround)
            {
                enemy.GetAgressive();
            }
        }
    }
}
