using UnityEngine;

public class HitDetector : MonoBehaviour
{
    public void HitReaction(Vector3 pos, Quaternion rot, GameObject target, int damage)
    {
            Enemy enemy = target.GetComponent<Enemy>();
            enemy.RecieveDamage(damage);
            enemy.Bleed(pos, rot);  
    }
}
