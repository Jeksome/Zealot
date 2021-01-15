using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public GameObject bleedEffect;
    public GameObject target;
    public Transform[] waypoints;

    private enum State {Patroling, Chasing, Attacking, Hitting, Dead, InHeaven}
    private State state;
    private Animator zombieAnim;
    private NavMeshAgent zombieAgent;
    private float attackRate;
    private float nextAttackTime;
    private int attackDamage;
    private int health;
    private int maxHealth;
    private int minHealth;
    private int waypointNumber;

    void Start()
    {
        zombieAgent = GetComponent<NavMeshAgent>();
        //zombieAgent.autoBraking = true;
        //MoveToNextWaypoint();
        state = State.Patroling;

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
                if (!zombieAgent.pathPending && zombieAgent.remainingDistance < 0.5f)
                    MoveToNextWaypoint();
                FindTarget();
                break;
            case State.Chasing:
                zombieAgent.speed = zombieAgent.speed;
                //zombieAgent.autoBraking = true;
                zombieAgent.destination = target.transform.position;
                zombieAnim.SetBool("isRunning", true);
                break;

            case State.Attacking:
                if (nextAttackTime < Time.time)
                {
                    state = State.Attacking;
                    zombieAnim.SetBool("isAttacking", true);
                    zombieAgent.isStopped = true;
                    nextAttackTime = Time.time + attackRate; 
                }
                break;
            case State.Dead:
                health = 0;
                zombieAnim.SetBool("isKilled", true);
                zombieAgent.isStopped = true;
                //GetComponent<CapsuleCollider>().direction = 2;
                //GetComponent<CapsuleCollider>().center = new Vector3(0, 0.23f, 0);
                state = State.InHeaven;
                break;
            case State.InHeaven:
                break;
        }
    }

    void MoveToNextWaypoint()
    {
        zombieAgent.destination = waypoints[waypointNumber].position;
        waypointNumber = Random.Range(0, waypoints.Length);
        //waypointNumber = (waypointNumber + 1) % waypoints.Length;             for bigger amount of waypoints
    }

    void FindTarget()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        //TODO SWAP TO ONTRIGGERENTER
        if (Physics.SphereCast(ray, 2f, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                state = State.Chasing;
            }
        }
    }

    void HitPlayer()    
    {
        //Function called twice as attack animation action
        attackDamage = Random.Range(1, 4);
        PlayerCharacter.instance.Hurt(attackDamage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "First Person Player")
        {
            zombieAnim.SetBool("isAttacking", true);
            state = State.Attacking;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "First Person Player")
        {
            zombieAnim.SetBool("isAttacking", false);
            zombieAgent.isStopped = false;
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
