using UnityEngine;

public class LockedDoor : Openable, IOpenable
{
    [SerializeField] private Key key;  

    void Start()
    {
        itemAnimator = GetComponent<Animator>();
        animationPlayingTimer = 1;
    }

    public override void Interact()
    {
        if (key.IsPickedUp == true && !animationIsPlaying && isOpen == false)
            Open();
        else if (!animationIsPlaying && isOpen == true)
            Close();
    } 
}
