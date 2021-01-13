using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public GameObject bleedEffect;
    public GameObject target;

    private float movingSpeed = 0.5f;
    private float obstacleRange = 1.0f;
    private float health = 5;
    private Animator zombieAnim;

    void Start()
    {
        zombieAnim = GetComponent<Animator>();
        zombieAnim.SetBool("isWalking", true);
    }

    void Update()
    {
        if (health == 5) Patroling();
        else if (health < 1)
        {
            zombieAnim.SetBool("isKilled", true);
            GetComponent<CapsuleCollider>().direction = 2;
            GetComponent<CapsuleCollider>().center = new Vector3(0, 0.23f, 0);
        }
        else Attack();
        }

    public void Hurt(int damage)
    {
        health -= damage;
        Debug.Log($"Zombie recieved {damage} damage and his health is {health} now");
        
        if (health <= 0)
            Debug.Log("You've killed a poor zombie! I hope you are happy now.");
    }

    public void Bleed(Vector3 pos, Quaternion rot)
    {
        GameObject spawnedDecal = GameObject.Instantiate(bleedEffect, pos, rot);
    }

    public void Attack()
    {
        Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
        transform.position += transform.forward * 4f * Time.deltaTime;
        zombieAnim.Play("Z_Run", 0, 0.25f);
    }

    private void Patroling()
    {
        transform.Translate(0, 0, movingSpeed * Time.deltaTime);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.GetComponent<PlayerCharacter>())
            {
                //TODO attack
            }
            if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }
}
