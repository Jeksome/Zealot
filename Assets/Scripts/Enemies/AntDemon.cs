using UnityEngine;
using UnityEngine.AI;

public class AntDemon : Enemy
{
    [SerializeField] private PlayerCharacter player;

    private void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        enemyAnimator.SetBool("isWalking", true);
        state = State.Patroling;

        Player = player;
        maxHealth = 9; 
        currentHealth = maxHealth;
        minHealth = 1;
        attackRate = 1.3f;
        sightDistance = 4f;
        detectDistance = 10f;
        runningSpeed = 7;
        waypointChangeDistance = 0.5f;
    }

    public override void HitPlayer()  
    {
        attackDamage = Random.Range(4, 10);
        player.Hurt(attackDamage);
    }
}
