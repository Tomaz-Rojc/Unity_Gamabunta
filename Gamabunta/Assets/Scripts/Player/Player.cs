using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public int armor = 0;
    public static int keys = 0;

    public TextMeshProUGUI healthDisplay;
    public Animator BossDoorAnimator;

    void Start()
    {
        health = maxHealth;
        keys = 0;
    }

    void Update()
    {
        if (health <= 0)
            Destroy(gameObject);

        if (health > maxHealth)
            health = maxHealth;

        if (healthDisplay != null)
            healthDisplay.SetText("HP: " + health + " / " + maxHealth);
        if (keys >= 5) {
            BossDoorAnimator.SetBool("openBossDoor", true);
        }
        //Debug.Log("Health " + health);
    }

    public void TakeDamage(int damage)
    {
        damage = damage - armor;
        if (damage <= 0) damage = 1;
        health -= damage;
    }
}
