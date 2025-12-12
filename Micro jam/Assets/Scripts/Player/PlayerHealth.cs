using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    public Image healthBar;       
    public GameObject deathScreen; 

    void Start()
    {
        currentHealth = maxHealth;

        UpdateHealthUI();
        if (deathScreen != null) deathScreen.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("Player Dead");

        if (deathScreen != null)
            deathScreen.SetActive(true);

        Time.timeScale = 0f;
    }
}
