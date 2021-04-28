using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AggroTrigger : MonoBehaviour, ITriggerable
{
    #pragma warning disable 0649
    [SerializeField] private List<Enemy> enemiesAround = new List<Enemy>();
    #pragma warning restore 0649

    public void ActivateTrigger() => StartCoroutine(ActivationDelay());

    private IEnumerator ActivationDelay()
    {
        yield return new WaitForSeconds(0.75f);
        foreach (Enemy enemy in enemiesAround)
        {
            enemy.GetAgressive();
        }       
    }
}
