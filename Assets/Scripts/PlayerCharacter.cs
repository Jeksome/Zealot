using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCharacter : MonoBehaviour
{
    public static PlayerCharacter instance = null;
    public TMP_Text healthAmount;

    public Material material1;
    public Material material2;
    public Material material3;
    public Material material4;
    public Material material5;
    public GameObject eye1;
    public GameObject eye2;
    public GameObject eyeGlow1;
    public GameObject eyeGlow2;

    public int health;
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

        Renderer rend = eye1.GetComponent<Renderer>();
        Renderer rend2 = eye2.GetComponent<Renderer>();
        Light light = eyeGlow1.GetComponent<Light>();
        Light light2 = eyeGlow2.GetComponent<Light>();

        if (health < 101 && health > 75)
        {
            rend.material = material2;
            rend2.material = material2;
            light.color = new Color(0, 1, 0.7f);
            light2.color = new Color(0, 1, 0.7f);
        }
        else if (health < 74 && health > 50)
        {
            rend.material = material1;
            rend2.material = material1;
            light.color = new Color(0, 0.6f, 0.9f);
            light2.color = new Color(0, 0.6f, 0.9f);
        }
        else if (health < 49 && health > 25)
        {
            rend.material = material3;
            rend2.material = material3;
            light.color = new Color(0.8f, 0.5f, 0.6f);
            light2.color = new Color(0.8f, 0.5f, 0.6f);
        }
        else if (health < 24 && health > 10)
        {
            rend.material = material4;
            rend2.material = material4;
            light.color = new Color(1, 0.1f, 0);
            light2.color = new Color(1, 0.1f, 0);
        }
        else if(health < 10)
        {
            rend.material = material5;
            rend2.material = material5;
            light.color = new Color(0.8f, 0, 0);
            light2.color = new Color(0.8f, 0, 0);
        }
    }

    public void Hurt(int damage)
    {
        health -= damage;
        Debug.Log("Health: " + health);
    }

    public void Heal(int healingAmount)
    {
        health += healingAmount;
        Debug.Log("Health: " + health);
    }

    public void Death()
    {
        Debug.Log("You are dead");
    }
}
