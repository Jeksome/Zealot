using UnityEngine;
using TMPro;

public class PlayerCharacter : MonoBehaviour
{
    public bool IsAlive { get { return isAlive; } }
    private bool isAlive = true;
    private const int minHealth = 1;
    [SerializeField] [Range(0, 100)] private int currentHealth;
    [SerializeField] private TMP_Text healthBar;

    void Start()
    {
        DisplayHealth(currentHealth);
        HealthCrystal.IsPickedUp += Heal;
    }

    public void Hurt(int damage = 1)
    {
        currentHealth -= damage;
        DisplayHealth(currentHealth);
        if (currentHealth < minHealth && isAlive)
            Die();
    }

    public void Heal(int healingAmount = 10)
    {
        currentHealth += healingAmount;
        if (currentHealth > 100)
            currentHealth = 100;

        DisplayHealth(currentHealth);
    }

    public void Die()
    {
        isAlive = false;
        DisplayHealth(0);
    }

    private void DisplayHealth(int health)
    {
        if (health > 0)
            healthBar.text = health.ToString();
        else
            healthBar.text = "Dead";
    }

    private void OnDisable()
    {
        HealthCrystal.IsPickedUp -= Heal;
    }
}
