using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour, IDoor
{
    public bool isOpen;
    private Animator doorAnim;
    private int doorWaitTimer = 1;
    private bool wait;
    bool hasCellKey;

    void Start()
    {
        doorAnim = GetComponent<Animator>();       
    }

    public void InsertKey()
    {
        hasCellKey = true;
    }

    public void OpenDoor()
    {
        if (hasCellKey == true && !wait && isOpen == false)
        {
            doorAnim.Play("DoorOpen", 0, 0.25f);
            isOpen = true;
            StartCoroutine(TimeBetweenInteractions());
        }
        else if(!wait && isOpen == true)
        {
            CloseDoor();
        }
    }

    public void CloseDoor()
    {
        doorAnim.Play("DoorClose", 0, 0.25f);
        isOpen = false;
        StartCoroutine(TimeBetweenInteractions());
    }

    private IEnumerator TimeBetweenInteractions()
    {
        wait = true;
        yield return new WaitForSeconds(doorWaitTimer);
        wait = false;
    }

}
