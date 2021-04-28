using System.Collections;
using UnityEngine;

public class Openable : MonoBehaviour, IOpenable
{
    protected Animator itemAnimator;
    protected float animationPlayingTimer;
    protected bool isOpen;
    protected bool isTriggered;
    protected bool animationIsPlaying;
    protected string openingAnimationName = "Open", closingAnimationName = "Close";

    public virtual void Interact()
    {
        if (!animationIsPlaying && !isOpen)
            Open();
        else if (!animationIsPlaying && isOpen)
            Close();
    }

    public void Open()
    {
        itemAnimator.Play(openingAnimationName, 0, 0.25f);
        TriggerEvent();
        isOpen = true;
        StartCoroutine(TimeBetweenInteractions());

        if (!isTriggered)
        {
            TriggerEvent();
            isTriggered = true;
        }
    }

    public void Close()
    {
        itemAnimator.Play(closingAnimationName, 0, 0.25f);
        isOpen = false;
        StartCoroutine(TimeBetweenInteractions());
    }

    public void TriggerEvent()
    {
        ITriggerable trigger = GetComponent<ITriggerable>();

        if (trigger != null)
        {
           trigger.ActivateTrigger();
        }
        else return;
    }

    private IEnumerator TimeBetweenInteractions()
    {
        animationIsPlaying = true;
        yield return new WaitForSeconds(animationPlayingTimer);
        animationIsPlaying = false;
    }
}
