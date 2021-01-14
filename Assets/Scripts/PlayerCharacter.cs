using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCharacter : MonoBehaviour
{
    public static PlayerCharacter instance = null;
    public TMP_Text healthAmount;

    private int health;
    private bool isAlive;
    void Start()
    {
        if (instance == null)
        {
            instance = this; 
        }
        else if (instance == this)
        { 
            Destroy(gameObject); 
        }

        health = 100;
        isAlive = true;
    }

    void Update()
    {
        if (health < 1 && isAlive)
        {
            Death();
            isAlive = false;
        }
        healthAmount.text = "Health: " + health.ToString();
    }

    public void Hurt(int damage)
    {
        health -= damage;
        Debug.Log("Health: " + health);
    }

    public void Death()
    {
        Debug.Log("You are dead");
    }
}
