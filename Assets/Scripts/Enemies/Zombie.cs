using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public enum State {Patroling, Chasing, Attacking, Hitting, Dead, InHeaven}
    public State state;
    public Transform[] waypoints;
    public GameObject bleedEffect;

    private Animator zombieAnim;
    private NavMeshAgent zombieAgent;
    private GameObject player;
    private float attackRate;
    private float nextAttackTime;
    private float distanceToPlayer;
    private int attackDamage;
    private int currentHealth, minHealth;
   
    void Start()
    {
        player = GameObject.Find("Player");

        zombieAgent = GetComponent<NavMeshAgent>();
        state = State.Patroling;

        zombieAnim = GetComponent<Animator>();
        zombieAnim.SetBool("isWalking", true);

        currentHealth = 5;
        minHealth = 1;
        attackRate = 0.75f;
    }

    void Update()
    {
        if (currentHealth < minHealth)
            state = State.Dead;

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        switch (state)
        {
            default:
            case State.Patroling:
                if (!zombieAgent.pathPending && zombieAgent.remainingDistance < 0.5f)
                    MoveToNextWaypoint();
                if (distanceToPlayer < 10f)
                    state = State.Chasing;
                break;
            case State.Chasing:
                zombieAgent.speed = 3;
                zombieAgent.destination = player.transform.position;
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

    private void OnTriggerStay(Collider other)
    {
            if (other.gameObject == player)
                state = State.Attacking;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            zombieAnim.SetBool("isAttacking", false);
            zombieAgent.isStopped = false;
            state = State.Chasing;
        }
    }

    void MoveToNextWaypoint()
    {
        int wpNumber = Random.Range(0, waypoints.Length); 
        zombieAgent.destination = waypoints[wpNumber].position;
    }

    void HitPlayer()  //Function called twice as attack animation action
    {
        attackDamage = Random.Range(1, 4);
        PlayerCharacter Player = player.GetComponent<PlayerCharacter>();
        Player.Hurt(attackDamage);
    }

    public void RecieveDamage(int damage)
    {
        if (state != State.InHeaven)
            currentHealth -= damage;
    }

    public void Bleed(Vector3 pos, Quaternion rot)
    {
        if (state != State.InHeaven) 
            Instantiate(bleedEffect, pos, rot);
    }
}
