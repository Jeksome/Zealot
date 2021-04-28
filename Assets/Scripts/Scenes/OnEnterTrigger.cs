using UnityEngine;

public class OnEnterTrigger : MonoBehaviour
{
    private bool isTriggered;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCharacter>() != null && !isTriggered)
        {
            GetComponent<ITriggerable>().ActivateTrigger();
            isTriggered = true;
        }
    }
}
