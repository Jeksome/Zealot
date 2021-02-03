using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCharacter : MonoBehaviour
{
    public int currentHealth, maxHealth, minHealth;
   
    [SerializeField] private TMP_Text healthBar;
    private bool isAlive;    

    void Start()
    {
        maxHealth = 100;
        minHealth = 1;
        currentHealth = 100;
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

    public void Heal(int healingAmount)
    {
        currentHealth += healingAmount;
    }

    public void Death()
    {
        Debug.Log("You are dead LOL");
    }
}
