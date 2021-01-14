using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public GameObject bleedEffect;
    public GameObject target;

    public enum State
    {
        Patroling,
        Chasing,
        Attacking,
        Hitting,
        Dead,
        InHeaven
    }

    private float movingSpeed = 0.5f;
    private float obstacleRange = 1.0f;
    private float attackRate;
    private int attackDamage;
    private int health;
    private int maxHealth;
    private int minHealth;
    private Animator zombieAnim;
    float nextAttackTime;

    private State state;

    private void Awake()
    {
        state = State.Patroling;
    }

    void Start()
    {
        zombieAnim = GetComponent<Animator>();
        zombieAnim.SetBool("isWalking", true);
        health = maxHealth = 5;
        minHealth = 1;
        attackRate = 0.75f;
    }

    void Update()
    {
        if (health < minHealth)
            state = State.Dead;
        if (health < maxHealth && health > minHealth)
            state = State.Chasing;

        switch (state)
        {
            default:
            case State.Patroling:
                transform.Translate(0, 0, movingSpeed * Time.deltaTime);

                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                if (Physics.SphereCast(ray, 0.75f, out hit))
                {
                    if (hit.distance < obstacleRange)
                    {
                        float angle = Random.Range(-110, 110);
                        transform.Rotate(0, angle, 0);
                    }
                }
                FindTarget();
                break;
            case State.Chasing:
                movingSpeed = 0.5f;
                Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
                transform.position += transform.forward * 4f * movingSpeed * Time.deltaTime;
                zombieAnim.SetBool("isRunning", true);
                break;

            case State.Attacking:
                if (nextAttackTime < Time.time)
                {
                    state = State.Attacking;
                    zombieAnim.SetBool("isAttacking", true);
                    movingSpeed = 0;
                    nextAttackTime = Time.time + attackRate;
                    attackDamage = Random.Range(1, 4);
                    PlayerCharacter.instance.Hurt(attackDamage);
                }
                break;
            case State.Dead:
                health = 0;
                zombieAnim.SetBool("isKilled", true);
                GetComponent<CapsuleCollider>().direction = 2;
                GetComponent<CapsuleCollider>().center = new Vector3(0, 0.23f, 0);
                state = State.InHeaven;
                break;
            case State.InHeaven:
                break;
        }
    }

    void FindTarget()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, 5f, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                state = State.Chasing;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "First Person Player")
        {
            zombieAnim.SetBool("isAttacking", true);
            movingSpeed = 0;
            state = State.Attacking;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "First Person Player")
        {
            zombieAnim.SetBool("isAttacking", false);
            movingSpeed = 0.5f;
            state = State.Chasing;
        }
    }

    public void Hurt(int damage)
    {
        health -= damage;
        Debug.Log($"Zombie recieved {damage} damage and his health is {health} now");
    }

    public void Bleed(Vector3 pos, Quaternion rot)
    {
        Instantiate(bleedEffect, pos, rot);
    }
}
