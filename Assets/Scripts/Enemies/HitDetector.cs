using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    public void HitReaction(Vector3 pos, Quaternion rot, GameObject target)
    {
            int damage = Random.Range(1, 3);
            Enemy enemy = target.GetComponent<Enemy>();
            enemy.RecieveDamage(damage);
            enemy.Bleed(pos, rot);  
    }
}
