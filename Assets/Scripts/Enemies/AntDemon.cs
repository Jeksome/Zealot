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
        state = State.PATROLING;

        Player = player;
        maxHealth = 9; 
        currentHealth = maxHealth;
        minHealth = 1;
        attackRate = 1.3f;
        sightDistance = 10f;
        runningSpeed = 8;
        waypointChangeDistance = 0.5f;
    }

    public override void HitPlayer()  
    {
        attackDamage = Random.Range(4, 10);
        player.GetHurt(attackDamage);
    }
}
