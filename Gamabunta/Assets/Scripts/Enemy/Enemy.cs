using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public int armor = 0;

    public Player player;

    public GameObject healthBarUI;
    public Slider slider;

    public GameObject weapon;

    public int restoreMana;
    public int restoreHealth;

    public GameObject youWon;

    public FrogController fc;
    private bool alreadyWon = false;
    void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();
    }

    void Update()
    {
        slider.value = CalculateHealth();

        if (health < maxHealth)
            healthBarUI.SetActive(true);

        if (health <= 0)
        {
            if (gameObject.tag == "Gamabunta") {
                alreadyWon = true;
                youWon.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                if (fc) 
                {
                    fc.Smashed();
                    fc.Guts();
                }
                if (GameObject.Find("Canvas")) healthBarUI.active = false;
                GetComponent<FinalBoss>().enabled = false;
                // add guts animation
            } else {
                Destroy(gameObject);
                player.GetComponentInChildren<Weapon>().bulletsLeft += restoreMana;
                player.health += restoreHealth;
            }
        }

        if (health > maxHealth)
            health = maxHealth;
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }

    public void TakeDamage(int damage)
    {
        damage = damage - armor;
        if (damage <= 0) damage = 1;
        health -= damage;
    }
}
