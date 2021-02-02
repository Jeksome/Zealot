using UnityEngine;
using UnityEngine.AI;

public class AntDemon : Enemy
{
    private void Start()
    {
        player = GameObject.Find("Player");
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        enemyAnimator.SetBool("isWalking", true);
        state = State.Patroling;

        maxHealth = 10; 
        currentHealth = maxHealth;
        minHealth = 1;
        attackRate = 0.75f;
        sightDistance = 10f;
        runningSpeed = 6;
        waypointChangeDistance = 0.5f;
    }

    public override void HitPlayer()    //method called as animation event
    {
        attackDamage = Random.Range(4, 10);
        PlayerCharacter Player = player.GetComponent<PlayerCharacter>();
        Player.Hurt(attackDamage);
    }
}
