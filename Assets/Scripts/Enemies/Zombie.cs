using UnityEngine;
using UnityEngine.AI;

public class Zombie : Enemy
{
    [SerializeField] private PlayerCharacter player;

    private void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        enemyAnimator.SetBool("isWalking", true);
        state = State.Patroling;

        Player = player;
        maxHealth = 5;
        currentHealth = maxHealth;
        minHealth = 1;
        attackRate = 0.5f;
        sightDistance = 7f; 
        runningSpeed = 3;
        waypointChangeDistance = 0.5f;
    }   

    public override void HitPlayer()  
    {
        attackDamage = Random.Range(1, 4);
        player.GetHurt(attackDamage);
    }
}
