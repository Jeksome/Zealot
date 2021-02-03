using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody projectileRb;
    private Vector3 projectileVelocity;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject smallHitEffect;  

    private void OnEnable()
    {
        projectileRb = GetComponent<Rigidbody>();
    }

    public void GetVelocity (Vector3 prVelocity)
    {
        projectileVelocity = prVelocity;
        //TODO Change to /get
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
            Blast(missblast);

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
