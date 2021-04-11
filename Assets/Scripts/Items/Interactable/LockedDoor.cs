using UnityEngine;

public class LockedDoor : Openable, IOpenable
{
    #pragma warning disable 0649
    [SerializeField] private Key key;
    #pragma warning restore 0649

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
