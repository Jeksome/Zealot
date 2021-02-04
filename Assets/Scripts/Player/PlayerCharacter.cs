using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCharacter : MonoBehaviour
{
    public bool IsAlive { get { return isAlive; } }

    private bool isAlive = true;
    private int currentHealth, maxHealth, minHealth;
    [SerializeField] private TMP_Text healthBar;   
    
    void Start()
    {
        maxHealth = 100;
        minHealth = 1;
        currentHealth = maxHealth;
        isAlive = true;        
    }

    void Update()
    {
        healthBar.text = currentHealth.ToString();

        if (currentHealth < minHealth && isAlive)
        {
            Death();
            isAlive = false;
        }      
    }

    public void Hurt(int damage = 1)
    {
        currentHealth -= damage;
    }

    public void Heal(int healingAmount = 10)
    {
        currentHealth += healingAmount;
    }

    public void Death()
    {
        Debug.Log("You are dead LOL");
    }
}
