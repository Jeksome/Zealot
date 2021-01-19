using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCharacter : MonoBehaviour
{
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
    public bool fNotPressed;

    public int currentHealth, maxHealth, minHealth;
    private bool isAlive;
    void Start()
    {
        maxHealth = 100;
        currentHealth = 15;
        isAlive = true;
        fNotPressed = true;
    }

    void Update()
    {
        if (currentHealth < 1 && isAlive)
        {
            Death();
            isAlive = false;
        }
        healthAmount.text = "Health: " + currentHealth.ToString();

        Renderer rend = eye1.GetComponent<Renderer>();
        Renderer rend2 = eye2.GetComponent<Renderer>();
        Light light = eyeGlow1.GetComponent<Light>();
        Light light2 = eyeGlow2.GetComponent<Light>();

        if (Input.GetKeyDown(KeyCode.F) && isAlive && fNotPressed)
        {  
            StartCoroutine(FlashLight());
            
        }

        IEnumerator FlashLight()
        {
            currentHealth -= 1;
            fNotPressed = false;
            light.range *= 12;
            light.intensity += 0.4f;
            yield return new WaitForSeconds(2f);
            light.range /= 12;
            light.intensity -= 0.4f;
            fNotPressed = true;
        }

        if (currentHealth < 101 && currentHealth > 75)
        {
            rend.material = material1;
            rend2.material = material1;
            light.color = new Color(0, 0.6f, 0.9f);
            light2.color = new Color(0, 0.6f, 0.9f);
        }
        else if (currentHealth < 74 && currentHealth > 50)
        {
            rend.material = material2;
            rend2.material = material2;
            light.color = new Color(0, 1, 1f);
            light2.color = new Color(0, 1, 1f);
        }
        else if (currentHealth < 49 && currentHealth > 25)
        {
            rend.material = material3;
            rend2.material = material3;
            light.color = new Color(0.8f, 0.5f, 0.6f);
            light2.color = new Color(0.8f, 0.5f, 0.6f);
        }
        else if (currentHealth < 24 && currentHealth > 10)
        {
            rend.material = material4;
            rend2.material = material4;
            light.color = new Color(1, 0.1f, 0);
            light2.color = new Color(1, 0.1f, 0);
        }
        else if(currentHealth < 10)
        {
            rend.material = material5;
            rend2.material = material5;
            light.color = new Color(0.8f, 0, 0);
            light2.color = new Color(0.8f, 0, 0);
        }
    }

    public void Hurt(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Health: " + currentHealth);
    }

    public void Heal(int healingAmount)
    {
        currentHealth += healingAmount;
        Debug.Log("Health: " + currentHealth);
    }

    public void Death()
    {
        Debug.Log("You are dead LOL");
    }
}
