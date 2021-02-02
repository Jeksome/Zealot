using UnityEngine;
using UnityEngine.AI;

public class Zombie : Enemy
{  
    void Start()
    {
        player = GameObject.Find("Player");
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        enemyAnimator.SetBool("isWalking", true);
        state = State.Patroling;

        maxHealth = 5;
        currentHealth = maxHealth;
        minHealth = 1;
        attackRate = 0.75f;
        sightDistance = 7f;
        runningSpeed = 3;
        waypointChangeDistance = 0.5f;
    }   

    public override void HitPlayer()  //Method called twice as attack animation action
    {
        attackDamage = Random.Range(1, 4);
        PlayerCharacter Player = player.GetComponent<PlayerCharacter>();
        Player.Hurt(attackDamage);
    }
}
