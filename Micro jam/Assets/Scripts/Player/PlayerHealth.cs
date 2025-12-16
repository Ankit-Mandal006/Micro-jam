using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;   // static reference
    public Animator animFlash,animShake;
    public int maxHealth = 5;
    public int currentHealth;

    public Image healthBar;
    public GameObject deathScreen;
    public AudioSource audioSource;


    void Awake()
    {
        // cache the single PlayerHealth so other scripts can call PlayerHealth.Instance
        Instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;

        UpdateHealthUI();
        if (deathScreen != null) deathScreen.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        audioSource.Play();
        currentHealth -= amount;
        UpdateHealthUI();
        animFlash.SetTrigger("Damage");
        animShake.SetTrigger("Shake");
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
    public void RestoreFullHealth()
    {
    currentHealth = maxHealth;
    UpdateHealthUI();
    }

    void Die()
    {
        Debug.Log("Player Dead");

        if (deathScreen != null)
            deathScreen.SetActive(true);

        Time.timeScale = 0f;
    }
}
