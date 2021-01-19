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
    Color playerColor;
    private void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
        material = GameObject.Find("Player").GetComponent<PlayerCharacter>().eye1.GetComponent<Renderer>().material;
        playerColor = GameObject.Find("Player").GetComponent<PlayerCharacter>().eyeGlow1.GetComponent<Light>().color;
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
        projectileRb.velocity = projectileVelocity.normalized * 22f;
    }

    private void OnCollisionEnter(Collision target)
    {
        HitDetector hitObject = target.transform.gameObject.GetComponent<HitDetector>();
        ContactPoint[] contacts = new ContactPoint[5];
        int numContacts = target.GetContacts(contacts);
        
        for (int i = 0; i < numContacts; i++)
        {
            if(contacts[i].otherCollider.gameObject.CompareTag("Enemy"))
            {
                int weaponDamage = Random.Range(1, 3);
                hitObject.HitReaction(contacts[i].point, transform.rotation, weaponDamage);
                GameObject effect = Instantiate(hitEffect, contacts[i].point, transform.rotation);
                Destroy(gameObject);
                Destroy(effect, 1f);
            }
            else { Destroy(gameObject, 0.1f); }
        }
    }
}
