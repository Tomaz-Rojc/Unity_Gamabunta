using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public int armor = 0;
    public static int keys = 0;

    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI keysDisplay;
    public Animator BossDoorAnimator;

    public Slider healthSlider;

    public GameObject blood1;
    public GameObject blood2;
    public GameObject blood3;
    public GameObject blood4;
    public GameObject bloodGradient;

    public AnimationManagerUI anim;

    void Start()
    {
        health = maxHealth;
        keys = 0;
    }

    void Update()
    {
        if (health <= 0) {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Death");
        }

        if (health > maxHealth)
            health = maxHealth;

        if (healthDisplay != null)
            healthDisplay.SetText("Health: " + health + " / " + maxHealth);
        if (keysDisplay != null)
            keysDisplay.SetText("Keys: " + keys);
        if (keys >= 5) {
            BossDoorAnimator.SetBool("openBossDoor", true);
            GameObject.Find("Gamabunta").GetComponent<FinalBoss>().enabled = true;
        }
        
        healthSlider.value = health/maxHealth;
        decreaseColor();
    }

    public void TakeDamage(int damage)
    {
        damage = damage - armor;
        if (damage <= 0) damage = 1;
        health -= damage;

        anim.SetAnimation_Damage();
        Debug.Log("Damage taken");
        bloodEffect();
    }

    public void bloodEffect()
    {
        var color = blood1.GetComponent<Image>().color;
        color.a = 0.8f;
        blood1.GetComponent<Image>().color = color;

        color = blood2.GetComponent<Image>().color;
        color.a = 0.8f;
        blood2.GetComponent<Image>().color = color;

        color = blood3.GetComponent<Image>().color;
        color.a = 0.8f;
        blood3.GetComponent<Image>().color = color;

        color = blood4.GetComponent<Image>().color;
        color.a = 0.8f;
        blood4.GetComponent<Image>().color = color;

        color = bloodGradient.GetComponent<Image>().color;
        color.a = 0.8f;
        bloodGradient.GetComponent<Image>().color = color;
    }

    public void decreaseColor()
    {
        var color = blood1.GetComponent<Image>().color;
        color.a -= 0.01f;
        blood1.GetComponent<Image>().color = color;

        color = blood2.GetComponent<Image>().color;
        color.a -= 0.01f;
        blood2.GetComponent<Image>().color = color;

        color = blood3.GetComponent<Image>().color;
        color.a -= 0.01f;
        blood3.GetComponent<Image>().color = color;

        color = blood4.GetComponent<Image>().color;
        color.a -= 0.01f;
        blood4.GetComponent<Image>().color = color;

        color = bloodGradient.GetComponent<Image>().color;
        color.a -= 0.005f;
        bloodGradient.GetComponent<Image>().color = color;
    }
}
