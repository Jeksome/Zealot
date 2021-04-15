using UnityEngine;
using UnityEngine.AI;

public class AntDemon : Enemy
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
        maxHealth = 9; 
        currentHealth = maxHealth;
        minHealth = 1;
        attackRate = 1.3f;
        runningSpeed = 8;
        sightDistance += 3;
    }

    public override void HitPlayer()  
    {
        attackDamage = Random.Range(4, 10);
        player.GetHurt(attackDamage);
    }
}
