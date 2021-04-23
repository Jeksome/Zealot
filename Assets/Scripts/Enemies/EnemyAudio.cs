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
    private bool isPlaying;

    public void StopPatrolingSound() => StartJob(growl);
    public virtual void PlayAttackSound() => StartJob(attack, true);
    public void PlayDyingSound() => StartJob(die);
    public void PlayOnHitRecieved() => StartJob(gotHit, true);

    protected void StartJob(AudioClip clip, bool randomChance = false)
    {
        if (!randomChance) source.PlayOneShot(clip);
        else PlayWithDelay(clip);    
    }


    private void PlayWithDelay(AudioClip clip)
    {
        int randomTime = Random.Range(4, 8);
        
        if (isPlaying) return;
        else
        {
            source.PlayOneShot(clip);
            isPlaying = true;
            StartCoroutine(IsPlaying(randomTime));
        }
    }

    private IEnumerator IsPlaying(int randomTime)
    {
        yield return new WaitForSeconds(randomTime);
        isPlaying = false;
    }
}
