using UnityEngine;
using UnityEngine.AI;

public class Zombie : Enemy
{  
    void Start()
    {
        player = GameObject.Find("Player");
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<Animator>();
        enemyAnim.SetBool("isWalking", true);
        state = State.Patroling;

        currentHealth = 5;
        maxHealth = 5;
        minHealth = 1;
        attackRate = 0.75f;
        sightDistance = 7.5f;
        runningSpeed = 3;
    }

    void Update()
    {
        if (currentHealth < minHealth)                                  
            state = State.Dead;
        if (currentHealth < maxHealth && !isDead && !isChasing)
            state = State.Chasing;

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        switch (state)
        {
            default:
            case State.Patroling:
                if (!enemyAgent.pathPending && enemyAgent.remainingDistance < 0.5f)
                    MoveToNextWaypoint();
                if (distanceToPlayer < sightDistance)
                    state = State.Chasing;
                break;
            case State.Chasing:
                isChasing = true;
                enemyAgent.speed = runningSpeed;
                enemyAgent.destination = player.transform.position;
                enemyAnim.SetBool("isRunning", true);
                break;
            case State.Attacking:
                if (nextAttackTime < Time.time)
                {
                    state = State.Attacking;
                    enemyAnim.SetBool("isAttacking", true);
                    enemyAgent.isStopped = true;
                    nextAttackTime = Time.time + attackRate; 
                }
                break;
            case State.Dead:
                currentHealth = 0;
                enemyAnim.SetBool("isDying", true);
                enemyAgent.isStopped = true;
                isDead = true;
                state = State.InHeaven;
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
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
            enemyAnim.SetBool("isAttacking", false);
            enemyAgent.isStopped = false;
            state = State.Chasing;
        }
    }

    public override void HitPlayer()  //Method called twice as attack animation action
    {
        attackDamage = Random.Range(1, 4);
        PlayerCharacter Player = player.GetComponent<PlayerCharacter>();
        Player.Hurt(attackDamage);
    }
}
