using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    public void HitReaction(Vector3 pos, Quaternion rot, int damage)
    {
        Zombie zombie = GameObject.Find("Zombie").GetComponent<Zombie>();

        if (zombie != null)
        {
            zombie.RecieveDamage(damage);
            zombie.Bleed(pos, rot);
        }
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        Target target = collider.GetComponent<Target>();
        if (target != null)
        {
            //hit target 
            target.DAMAGE();
            Destroy(gameObject);
        }
    }*/
}
