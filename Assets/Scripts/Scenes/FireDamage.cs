using System.Collections;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    private bool isTriggered;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCharacter>() && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(VoidZone(other.gameObject.GetComponent<PlayerCharacter>()));
        }
    }

    private IEnumerator VoidZone(PlayerCharacter player)
    {
        yield return new WaitForSeconds(1.5f);
        player.GetHurt(5);
        isTriggered = false;
    }
}
