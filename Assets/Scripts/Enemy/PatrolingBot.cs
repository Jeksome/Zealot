using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolingBot : MonoBehaviour
{
    private Animator zombieAnim;

    private float movingSpeed = 1.0f;
    private float obstacleRange = 1.0f;
    private float health = 5;

    private bool alive;

    void Start()
    {
        alive = true;
        zombieAnim = GetComponent<Animator>();
        zombieAnim.SetBool("isWalking", true);
    }

    void Update()
    {
        if (health < 1)
            alive = false;

        if (alive)
            Patroling();
        else
        {
            zombieAnim.SetBool("isKilled", true);
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    public void Hurt(int damage)
    {
        health -= damage;
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
             
            }
            if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }
}
