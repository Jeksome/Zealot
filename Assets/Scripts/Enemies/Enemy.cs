using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    protected enum State { Patroling, Chasing, Attacking, Hitting, Dying, Dead }
    protected State state;
    protected NavMeshAgent enemyAgent;
    protected Animator enemyAnimator;
    protected PlayerCharacter Player;
    protected int currentHealth, minHealth, maxHealth;
    protected int attackDamage;
    protected int runningSpeed;
    protected float distanceToPlayer, sightDistance, waypointChangeDistance;
    protected float nextAttackTime;
    protected float attackRate;
    protected bool isChasing, isDead;

    public delegate void EnemyKilled();
    public static event EnemyKilled enemyKilled;

    public Transform[] waypoints;

    public virtual void FixedUpdate()
    {
        if (currentHealth < minHealth && !isDead)
            state = State.Dying;
        if (currentHealth < maxHealth && !isDead && !isChasing)
            state = State.Chasing;

        distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        switch (state)
        {
            case State.Patroling:
                Patrol();
                break;
            case State.Chasing:
                Chase();
                break;
            case State.Attacking:
                Attack();
                break;
            case State.Dying:
                Die();
                break;
            case State.Dead:
                break;
        }
    }
    public virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Player.gameObject && !isDead)
            state = State.Attacking;
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player.gameObject && !isDead)
            StartCoroutine(ContinueChasing(attackRate));
    }
    public void Patrol()
    {
        if (!enemyAgent.pathPending && enemyAgent.remainingDistance < waypointChangeDistance)
            MoveToNextWaypoint();
        if (distanceToPlayer < sightDistance)
            state = State.Chasing;
    }

    public void MoveToNextWaypoint()
    {
        int wpNumber = Random.Range(0, waypoints.Length);
        enemyAgent.destination = waypoints[wpNumber].position;
    }

    public void Chase()
    {
        isChasing = true;
        enemyAgent.speed = runningSpeed;
        enemyAgent.acceleration += 2;
        enemyAgent.destination = Player.gameObject.transform.position;
        enemyAnimator.SetBool("isRunning", true);
    }

    private IEnumerator ContinueChasing(float attackDelay)
    {
        yield return new WaitForSeconds(attackDelay);
        enemyAnimator.SetBool("isAttacking", false);
        enemyAgent.isStopped = false;
        state = State.Chasing;
    }

    public void Attack()
    {
        if (nextAttackTime < Time.time)
        {
            state = State.Attacking;
            enemyAnimator.SetBool("isAttacking", true);
            enemyAgent.isStopped = true;
            nextAttackTime = Time.time + attackRate;
        }
    }

    public abstract void HitPlayer();  //Method called as attack animation action   

    public void RecieveDamage(int damage)
    {
        if (state != State.Dying)
            currentHealth -= damage;
    }

    public void Bleed(Vector3 pos, Quaternion rot)
    {
        if (state != State.Dying)
        {
            GameObject bleedEffect = ObjectPooler.SharedInstance.GetPooledObject("Blood");
            if (bleedEffect != null)
            {
                bleedEffect.transform.position = pos;
                bleedEffect.transform.rotation = rot;
                bleedEffect.SetActive(true);
            }
        }
    }

    public void Die()
    {
        currentHealth = 0;
        enemyAnimator.SetBool("isDying", true);
        enemyAgent.isStopped = true;
        isDead = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;

        if (enemyKilled != null)
            enemyKilled();

        state = State.Dead;
    }
}
