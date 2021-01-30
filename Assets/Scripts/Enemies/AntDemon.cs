using UnityEngine;
using UnityEngine.AI;

public class AntDemon : Enemy
{
    private void Start()
    {
        player = GameObject.Find("Player");
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<Animator>();
        enemyAnim.SetBool("isWalking", true);
        state = State.Patroling;

        maxHealth = 10; 
        currentHealth = maxHealth;
        minHealth = 1;
        attackRate = 0.75f;
        sightDistance = 8f;
        runningSpeed = 6;
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

    public override void HitPlayer()    //method called as animation event
    {
        attackDamage = Random.Range(4, 10);
        PlayerCharacter Player = player.GetComponent<PlayerCharacter>();
        Player.Hurt(attackDamage);
    }
}
