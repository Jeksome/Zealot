using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCharacter : MonoBehaviour
{
    public int currentHealth, maxHealth, minHealth;
    public GameObject eyeGlow;
    public GameObject eye;

    [SerializeField] private TMP_Text healthBar;
    private bool isAlive;
    private bool buttonNotPressed;

    void Start()
    {
        maxHealth = 100;
        minHealth = 1;
        currentHealth = 15;
        isAlive = true;
        buttonNotPressed = true;
    }

    void Update()
    {
        healthBar.text = currentHealth.ToString();

        if (currentHealth < minHealth /*&& isAlive*/)
        {
            Death();
            isAlive = false;
        }

        Renderer rend = eye.GetComponent<Renderer>();
        Light light = eyeGlow.GetComponent<Light>();

        if (Input.GetKeyDown(KeyCode.F) && isAlive && buttonNotPressed)
        {  
            StartCoroutine(FlashLight());           
        }

        IEnumerator FlashLight()
        {
            currentHealth -= 1;
            buttonNotPressed = false;
            light.range *= 12;
            light.intensity += 0.4f;
            yield return new WaitForSeconds(2f);
            light.range /= 12;
            light.intensity -= 0.4f;
            buttonNotPressed = true;
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
