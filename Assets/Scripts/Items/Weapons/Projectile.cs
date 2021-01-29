using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody projectileRb;
    Vector3 projectileVelocity;
    Material material;
    Renderer rend;
    public GameObject forMaterial;
    public GameObject forGlow;
    public GameObject hitEffect;
    public GameObject smallHitEffect;
    Color playerColor;
    private void OnEnable()
    {
        projectileRb = GetComponent<Rigidbody>();
        material = GameObject.Find("Player").GetComponent<PlayerCharacter>().eye.GetComponent<Renderer>().material;
        playerColor = GameObject.Find("Player").GetComponent<PlayerCharacter>().eyeGlow.GetComponent<Light>().color;
        forMaterial.GetComponent<Renderer>().material = material;
        forGlow.GetComponent<Light>().color = playerColor;
    }

    public void GetVelocity (Vector3 prVelocity)
    {
        projectileVelocity = prVelocity;
    }

    private void Update()
    {  
        projectileRb.rotation = Quaternion.LookRotation(projectileRb.velocity);
        projectileRb.velocity = projectileVelocity.normalized * 32f;
    }

    private void OnCollisionEnter(Collision target)
    {
        HitDetector hitObject = target.transform.gameObject.GetComponent<HitDetector>();
        ContactPoint[] contacts = new ContactPoint[5];
        int numContacts = target.GetContacts(contacts);
        forGlow.GetComponent<Light>().intensity = 1;

        for (int i = 0; i < numContacts; i++)
        {
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
                        StartCoroutine(Fade(blast, 0.15f));
                    }
                }
                StartCoroutine(Fade(gameObject, 0.2f));         
            }
            else 
            {
                GameObject blast = ObjectPooler.SharedInstance.GetPooledObject("ExplosionSmall");
                StartCoroutine(Fade(blast, 0.15f));
                StartCoroutine(Fade(gameObject, 0.2f));
            }
        }

        IEnumerator Fade(GameObject effect, float time)
        {
            yield return new WaitForSeconds(time);
            effect.SetActive(false);
        }
    }
}
