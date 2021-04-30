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
        enemyAudio = GetComponent<EnemyAudio>();
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
        enemyAudio.PlayAttackSound();
        attackDamage = Random.Range(8, 12);
        player.GetHurt(attackDamage);
    }
}
