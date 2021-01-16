using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private enum State {Patroling, Chasing, Attacking, Hitting, Dead, InHeaven}
    private State state;
    private Animator zombieAnim;
    private NavMeshAgent zombieAgent;
    private float attackRate;
    private float nextAttackTime;
    private int attackDamage;
    private int currentHealth;
    private int maxHealth;
    private int minHealth;
    private int waypointNumber;

    public GameObject bleedEffect;
    public GameObject target;
    public Transform[] waypoints;

    void Start()
    {
        zombieAgent = GetComponent<NavMeshAgent>();
        state = State.Patroling;

        zombieAnim = GetComponent<Animator>();
        zombieAnim.SetBool("isWalking", true);

        currentHealth = maxHealth = 5;
        minHealth = 1;
        attackRate = 0.75f;
    }

    void Update()
    {
        if (currentHealth < minHealth)
            state = State.Dead;
        if (currentHealth < maxHealth && currentHealth > minHealth)
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
                currentHealth = 0;
                zombieAnim.SetBool("isKilled", true);
                zombieAgent.isStopped = true;
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
        //TODO CHANGE FUNCTIONALITY TO ONTRIGGERENTER
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
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            Debug.Log($"Zombie recieved {damage} damage and his health is {currentHealth} now");
        }
    }

    public void Bleed(Vector3 pos, Quaternion rot)
    {
        if (currentHealth > 0)
        { 
        Instantiate(bleedEffect, pos, rot);
        }
    }
}
