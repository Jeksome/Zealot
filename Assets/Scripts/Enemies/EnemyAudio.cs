using System.Collections;
using UnityEngine;

public abstract class EnemyAudio : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] protected AudioClip attack;
    [SerializeField] protected AudioClip die;
    [SerializeField] protected AudioClip gotHit;
    [SerializeField] protected AudioClip growl;
    #pragma warning restore 0649

    protected AudioSource source;
    private int randomTime;

    public void StopPatrolingSound()
    {
        StartJob(growl);
    }

    public void PlayAttackSound()
    {
        StartJob(attack, true);
    }

    public void PlayDyingSound()
    {
        StartJob(die);
    }

    public void PlayOnHitRecieved()
    {
        StartJob(gotHit, true);
    }

    private void StartJob(AudioClip clip, bool randomChance = false, int minDelay = 0, int maxDelay = 0)
    {
        StopAllCoroutines();
        StartCoroutine(PlaySound(clip, randomChance, minDelay, maxDelay));
    }

    private IEnumerator PlaySound(AudioClip clip, bool randomChance, int minDelay = 0, int MaxDelay = 0)
    {
        yield return new WaitForSeconds(randomTime);
        randomTime = Random.Range(minDelay, MaxDelay);
        if (!randomChance) source.PlayOneShot(clip);
        else
        {
            int dice = Random.Range(1, 4);
            if (dice == 1)
            {
                source.PlayOneShot(clip);
            }
        }

    }
}
