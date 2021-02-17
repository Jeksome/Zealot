using System.Collections;
using UnityEngine;

public class Openable : MonoBehaviour, IOpenable
{
    protected Animator itemAnimator;
    protected float animationPlayingTimer;
    protected bool isOpen;
    protected bool animationIsPlaying;
    protected string openingAnimationName = "Open", closingAnimationName = "Close";

    public virtual void Interact()
    {
        if (!animationIsPlaying && isOpen == false)
            Open();
        else if (!animationIsPlaying && isOpen == true)
            Close();
    }

    public void Open()
    {
        itemAnimator.Play(openingAnimationName, 0, 0.25f);
        isOpen = true;
        StartCoroutine(TimeBetweenInteractions());
    }

    public void Close()
    {
        itemAnimator.Play(closingAnimationName, 0, 0.25f);
        isOpen = false;
        StartCoroutine(TimeBetweenInteractions());
    }

    private IEnumerator TimeBetweenInteractions()
    {
        animationIsPlaying = true;
        yield return new WaitForSeconds(animationPlayingTimer);
        animationIsPlaying = false;
    }
}
