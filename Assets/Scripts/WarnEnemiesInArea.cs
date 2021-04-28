using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarnEnemiesInArea : MonoBehaviour
{
    private List<Enemy> enemiesInArea = new List<Enemy>();
    private float aggresionDelay = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Projectile>())
        {
            StartCoroutine(DelayAggression());
        }

        if (other.gameObject.GetComponent<Enemy>())
        {
            enemiesInArea.Add(other.gameObject.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            enemiesInArea.Remove(other.gameObject.GetComponent<Enemy>());
        }
    }

    private IEnumerator DelayAggression()
    {
        yield return new WaitForSeconds(aggresionDelay);
        foreach (Enemy enemy in enemiesInArea)
        {
            enemy.GetAgressive();
        }
    }
}
