using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    protected enum State { Patroling, Chasing, Attacking, Hitting, Dead, InHeaven }
    protected State state;
    protected NavMeshAgent enemyAgent;
    protected GameObject player;
    protected Animator enemyAnim;
    protected int currentHealth, minHealth, maxHealth;
    protected int attackDamage;
    protected int runningSpeed;
    protected float distanceToPlayer;
    protected float sightDistance;
    protected float nextAttackTime;
    protected float attackRate;
    protected bool isChasing, isDead;
    
    public Transform[] waypoints;

    public abstract void HitPlayer();  //Method called as attack animation action   
    public void RecieveDamage(int damage)
    {
        if (state != State.InHeaven)
            currentHealth -= damage;
    }

    public void Bleed(Vector3 pos, Quaternion rot)
    {
        if (state != State.InHeaven)
        {
            GameObject bleedEffect = ObjectPooler.SharedInstance.GetPooledObject("Blood");
            if (bleedEffect != null)
            {
                bleedEffect.transform.position = pos;
                bleedEffect.transform.rotation = rot;
                bleedEffect.SetActive(true);
            }
        }
    }

    public void MoveToNextWaypoint()
    {
        int wpNumber = Random.Range(0, waypoints.Length);
        enemyAgent.destination = waypoints[wpNumber].position;
    }

     
}
