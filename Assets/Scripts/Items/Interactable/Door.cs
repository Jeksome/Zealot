using UnityEngine;

public class Door : Openable
{
    void Start()
    {
        itemAnimator = GetComponent<Animator>();
        animationPlayingTimer = 1;       
    }
}
