using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private const int minHealth = 1;
    private const int maxStat = 100;
    private int currentHealth;
    private int currentArmor;

    public bool CanCast { get { return canCast; } }
    private bool canCast = true;
    public bool IsBurdened { get { return isBurdened; } }
    private bool isBurdened;   

    #pragma warning disable 0649
    [SerializeField] HealthBar healthBar;
    [SerializeField] ArmorBar armorBar;    
    #pragma warning restore 0649

    private void Start()
    {
        currentHealth = 25;
        HealthCrystal.IsPickedUp += Heal;
        Armor.isPickedUp += AddArmor;
    }

    private void Update()
    {
        healthBar.Display(currentHealth);
        armorBar.Display(currentArmor);

        if (currentArmor > 0)
            isBurdened = true;
        else
            isBurdened = false;
    }

    public void GetHurt(int damage)
    {
        if (currentArmor <= 0)
        {
            currentHealth -= damage;
            currentArmor = 0;
        }
        else if (currentArmor > 0)
        {
            currentHealth -= damage / 2;
            currentArmor -= 1;
        }

        if (currentHealth < minHealth)
            Die();
    }

    public void GetHurt()
    {
        currentHealth -= 1;       

        if (currentHealth <= minHealth)
        {
            currentHealth = 1;
            canCast = false;
        }
    }

    public void Heal(int healingAmount = 10)
    {
        currentHealth += healingAmount;
        if (currentHealth > maxStat)
            currentHealth = maxStat;

        if (currentHealth > 1)
            canCast = true;
    }

    public void AddArmor(int armorAmount)
    {
        currentArmor += armorAmount;
        if (currentArmor > maxStat)
            currentArmor = maxStat;
    }

    public void Die() => GameManager.Instance.GameOver();

    private void OnDisable()
    {
        HealthCrystal.IsPickedUp -= Heal;
        Armor.isPickedUp -= AddArmor;
    }
}
