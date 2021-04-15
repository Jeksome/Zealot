using UnityEngine;
using UnityEngine.AI;

public class Zombie : Enemy
{
    #pragma warning disable 0649
    [SerializeField] private PlayerCharacter player;
    #pragma warning restore 0649

    private void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        enemyAnimator.SetBool("isWalking", true);

        Player = player;
        maxHealth = 5;
        currentHealth = maxHealth;
        minHealth = 1;
        attackRate = 0.5f;
        runningSpeed = 5;
    }   

    public override void HitPlayer()  
    {
        attackDamage = Random.Range(1, 4);
        player.GetHurt(attackDamage);
    }
}
