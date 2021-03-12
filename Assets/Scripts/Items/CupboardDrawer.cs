using UnityEngine;

public class CupboardDrawer : Openable
{
    void Start()
    {
        itemAnimator = GetComponent<Animator>();
        animationPlayingTimer = 0.75f;
        openingAnimationName = "DrawerOpen";
        closingAnimationName = "DrawerClose";
    }
}
