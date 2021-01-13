using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour
{   
    public void HitReaction(Vector3 pos, Quaternion rot, int damage)
    {
        Zombie zombie = GetComponent<Zombie>();
        if (zombie != null)
        {
            zombie.Hurt(damage);
            zombie.Bleed(pos, rot);
        }
    }
}
