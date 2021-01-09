using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private int _health;
    private bool isAlive;
    void Start()
    {
        _health = 5;
        isAlive = true;
    }

    void Update()
    {
        if (_health < 1 && isAlive)
        {
            Death();
            isAlive = false;
        }
    }

    public void Hurt(int damage)
    {
        _health -= damage;
        Debug.Log("Health: " + _health);
    }

    public void Death()
    {
        Debug.Log("You are dead");
    }
}
