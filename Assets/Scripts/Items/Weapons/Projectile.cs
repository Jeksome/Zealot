using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody projectileRb;
    Vector3 projectileVelocity;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject smallHitEffect;

    private void OnEnable()
    {
        projectileRb = GetComponent<Rigidbody>();
    }

    public void GetVelocity (Vector3 prVelocity)
    {
        projectileVelocity = prVelocity;
    }

    private void FixedUpdate()
    {
        projectileRb.rotation = Quaternion.LookRotation(projectileRb.velocity);
        projectileRb.velocity = projectileVelocity.normalized * 32f;
    }

    private void OnCollisionEnter(Collision target)
    {
        HitDetector hitObject = target.transform.gameObject.GetComponent<HitDetector>();
        ContactPoint[] contacts = new ContactPoint[1];
        int numContacts = target.GetContacts(contacts);

        for (int i = 0; i < numContacts; i++)
        {
            Debug.Log(contacts[i].otherCollider.gameObject.name);
            if(contacts[i].otherCollider.gameObject.CompareTag("Enemy"))
            {
                
                hitObject.HitReaction(contacts[i].point, transform.rotation, target.gameObject);
                GameObject blast = ObjectPooler.SharedInstance.GetPooledObject("ExplosionBig");
                {
                    if (blast != null)
                    {
                        blast.transform.position = contacts[i].point;
                        blast.transform.rotation = transform.rotation;
                        blast.SetActive(true);
                    }
                }
                gameObject.SetActive(false);
            }
            else 
            {
                GameObject blast = ObjectPooler.SharedInstance.GetPooledObject("ExplosionSmall");
                blast.transform.position = contacts[i].point;
                blast.transform.rotation = transform.rotation;
                blast.SetActive(true);
                gameObject.SetActive(false);
            }
        }        
    }
    IEnumerator Fade(GameObject effect, float time)
    {
        yield return new WaitForSeconds(time);
        effect.SetActive(false);
    }
}
