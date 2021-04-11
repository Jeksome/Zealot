using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    protected enum State { PATROLING, CHASING, ATTACKING, DYING, DEAD }
    protected State state = State.PATROLING;
    protected NavMeshAgent enemyAgent;
    protected Animator enemyAnimator;
    protected PlayerCharacter Player;
    protected int currentHealth, minHealth, maxHealth;
    protected int attackDamage;
    protected int runningSpeed;
    protected float distanceToPlayer, sightDistance, waypointChangeDistance;
    protected float nextAttackTime;
    protected float attackRate;
    protected bool isChasing;

    #pragma warning disable 0649
    [SerializeField] private Transform[] waypoints;
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
        if (other.gameObject == Player.gameObject && currentHealth > 0)
            state = State.ATTACKING;
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player.gameObject && state != State.DEAD)
            StartCoroutine(ContinueChasing(attackRate));
    }
    protected void Patrol()
    {
        if (!enemyAgent.pathPending && enemyAgent.remainingDistance < waypointChangeDistance)
            MoveToNextWaypoint();
        else if (distanceToPlayer < sightDistance)
            state = State.CHASING;
    }

    protected void MoveToNextWaypoint()
    {
        int wpNumber = Random.Range(0, waypoints.Length);
        enemyAgent.destination = waypoints[wpNumber].position;
    }

    protected void Chase()
    {
        isChasing = true;
        enemyAgent.speed = runningSpeed;
        enemyAgent.acceleration += 4;
        enemyAgent.destination = Player.gameObject.transform.position;
        enemyAnimator.SetBool("isRunning", true);
    }

    private IEnumerator ContinueChasing(float attackDelay)
    {
        yield return new WaitForSeconds(attackDelay);
        enemyAnimator.SetBool("isAttacking", false);
        enemyAgent.isStopped = false;
        state = State.CHASING;
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
            currentHealth -= damage;

            if (isChasing != true)
            {
                state = State.CHASING;
                isChasing = true;
            }
               
            if (currentHealth < minHealth)
                state = State.DYING;
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
        currentHealth = 0;
        enemyAnimator.SetBool("isDying", true);
        enemyAgent.isStopped = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        state = State.DEAD;
    }
}
