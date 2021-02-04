using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 ProjectileVelocity { set { projectileVelocity = value; } }
    private Vector3 projectileVelocity;
    private Rigidbody projectileRb;    
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject smallHitEffect;   

    private void OnEnable()
    {
        projectileRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        projectileRb.velocity = projectileVelocity;
    }

    private void OnCollisionEnter(Collision target)
    {
        HitDetector hitObject = target.transform.gameObject.GetComponent<HitDetector>();
        GameObject hitblast = ObjectPooler.SharedInstance.GetPooledObject("ExplosionBig");
        GameObject missblast = ObjectPooler.SharedInstance.GetPooledObject("ExplosionSmall");

        if (target.gameObject.CompareTag("Enemy"))
        {
            Blast(hitblast);
            hitObject.HitReaction(transform.position, transform.rotation, target.gameObject);
        }
        else
        {
            Blast(missblast);
        }
        gameObject.SetActive(false);
    }

    private void Blast (GameObject blastEffect)
    {
        if (blastEffect != null)
        {
            blastEffect.transform.position = transform.position;
            blastEffect.SetActive(true);            
        }
    }
}
