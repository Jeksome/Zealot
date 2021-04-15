﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    private enum State { PATROLING, CHASING, ATTACKING, DYING, DEAD }
    private State state = State.PATROLING;
    private bool isChasing;
    private float distanceToPlayer;

    protected NavMeshAgent enemyAgent;
    protected Animator enemyAnimator;
    protected PlayerCharacter Player;
    protected int currentHealth, minHealth, maxHealth;
    protected int attackDamage;
    protected int runningSpeed;   
    protected float nextAttackTime;
    protected float attackRate;       
    protected float waypointChangeDistance = 0.5f;

    #pragma warning disable 0649
    [SerializeField] private Transform[] waypoints;
    [SerializeField] protected float sightDistance = 7f;
    [SerializeField] protected List<Enemy> enemiesNearby;
    #pragma warning restore 0649

    protected void Update() => distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

    protected void FixedUpdate()
    {      
        switch (state)
        {
            case State.PATROLING:
                Patrol();
                break;
            case State.CHASING:
                Chase();
                break;
            case State.ATTACKING:
                Attack();
                break;
            case State.DYING:
                Die();
                break;
            case State.DEAD:
                break;
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Player.gameObject && state != State.DEAD)
            state = State.ATTACKING;
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player.gameObject && state != State.DEAD && state != State.DYING)
            StartCoroutine(ContinueChasing(attackRate));
    }
    protected void Patrol()
    {
        if (!enemyAgent.pathPending && enemyAgent.remainingDistance < waypointChangeDistance && state != State.DEAD)
            MoveToNextWaypoint();
        else if (distanceToPlayer < sightDistance)
        {
            BecomeAgressive();
            WarnOtherEnemies();
        }
    }

    protected void MoveToNextWaypoint()
    {
        int wpNumber = Random.Range(0, waypoints.Length);
        enemyAgent.destination = waypoints[wpNumber].position;
    }

    protected void WarnOtherEnemies()
    {
        if (enemiesNearby == null)
        {
            return;
        }
        else
        {
            foreach (Enemy enemy in enemiesNearby)
            {
                enemy.BecomeAgressive();
            }
        }
    }

    public void BecomeAgressive() => state = State.CHASING;

    protected void Chase()
    {
        if (state != State.DEAD)
        {
            isChasing = true;
            enemyAgent.speed = runningSpeed;
            enemyAgent.acceleration += 4;
            enemyAgent.destination = Player.gameObject.transform.position;
            enemyAnimator.SetBool("isRunning", true);
        }       
    }

    private IEnumerator ContinueChasing(float attackDelay)
    {       
        enemyAnimator.SetBool("isAttacking", false);
        enemyAgent.isStopped = false;
        state = State.CHASING;
        yield return new WaitForSeconds(attackDelay);
    }

    protected void Attack()
    {
        if (nextAttackTime < Time.time)
        {
            state = State.ATTACKING;
            enemyAnimator.SetBool("isAttacking", true);
            enemyAgent.isStopped = true;
            nextAttackTime = Time.time + attackRate;           
        }       
    }

    public abstract void HitPlayer();  //Method called as attack animation action   

    public void RecieveDamage(int damage)
    {
        if (state != State.DEAD)
        {
            if (currentHealth < minHealth)
                state = State.DYING;

            currentHealth -= damage;

            if (isChasing != true)
            {
                BecomeAgressive();
                WarnOtherEnemies();
            }                  
        }
    }

    public void Bleed(Vector3 pos, Quaternion rot)
    {
            GameObject bleedEffect = ObjectPooler.Instance.GetPooledObject("Blood");
            if (bleedEffect != null)
            {
                bleedEffect.transform.position = pos;
                bleedEffect.transform.rotation = rot;
                bleedEffect.SetActive(true);
            }
    }

    protected void Die()
    {
        state = State.DEAD;

        currentHealth = 0;
        enemyAnimator.SetBool("isDying", true);
        enemyAgent.isStopped = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;       
    }
}
