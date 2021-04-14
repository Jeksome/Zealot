using System.Collections.Generic;
using UnityEngine;

public class FallOnHay : MonoBehaviour
{
    private bool isTriggered;

    #pragma warning disable 0649
    [SerializeField] PlayerCharacter player;
    [SerializeField] AudioClip impactSound;
    [SerializeField] List<Enemy> enemiesAround = new List<Enemy>();
    #pragma warning restore 0649

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject && isTriggered == false)
        {
            isTriggered = true;
            Debug.Log("is triggered");
            AudioPlayer.PlayAudio(impactSound, gameObject.transform.position);
            player.GetHurt(10);
            foreach (Enemy enemy in enemiesAround)
            {
                enemy.BecomeAgressive();
            }
        }
    }
}
