using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public int armor = 0;

    public TextMeshProUGUI healthDisplay;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (health <= 0)
            Destroy(gameObject);

        if (health > maxHealth)
            health = maxHealth;

        if (healthDisplay != null)
            healthDisplay.SetText("HP: " + health + " / " + maxHealth);
    }

    public void TakeDamage(int damage)
    {
        damage = damage - armor;
        if (damage <= 0) damage = 1;
        health -= damage;
    }
}
