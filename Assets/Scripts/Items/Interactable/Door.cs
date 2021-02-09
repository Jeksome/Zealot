using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IOpenable
{
    private Animator doorAnimator;
    private int doorWaitTimer = 1;
    private bool isOpen;
    private bool animationIsPlaying;      

    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (!animationIsPlaying && isOpen == false)
        {
            OpenDoor();
        }
        else if (!animationIsPlaying && isOpen == true)
        {
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        doorAnimator.Play("DoorOpen", 0, 0.25f);
        isOpen = true;
        StartCoroutine(TimeBetweenInteractions());
    }

    public void CloseDoor()
    {
        doorAnimator.Play("DoorClose", 0, 0.25f);
        isOpen = false;
        StartCoroutine(TimeBetweenInteractions());
    }

    private IEnumerator TimeBetweenInteractions()
    {
        animationIsPlaying = true;
        yield return new WaitForSeconds(doorWaitTimer);
        animationIsPlaying = false;
    }
}
