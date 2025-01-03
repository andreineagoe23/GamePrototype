using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private float targetHealth; // Target value for smooth animation

    // Health bar settings
    public Slider healthSlider;        
    public TextMeshProUGUI healthText;

    // Damage flash settings
    public Image damageFlashImage; 
    public float flashSpeed = 5f; 
    public Color flashColor = new Color(1f, 0f, 0f, 0.5f); 
    private bool isDamaged = false;

    // Death screen settings
    public GameObject deathScreen; 

    public float smoothSpeed = 10f; // Speed for smooth animation

    void Start()
    {
        currentHealth = maxHealth;
        targetHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }

        if (healthText != null)
        {
            healthText.text = $"{currentHealth}/{maxHealth}";
        }

        if (deathScreen != null)
        {
            deathScreen.SetActive(false);
        }
    }

    void Update()
    {
        // Smoothly update the health bar
        if (healthSlider != null)
        {
            healthSlider.value = Mathf.Lerp(healthSlider.value, targetHealth, Time.deltaTime * smoothSpeed);
        }

        // Handle the damage flash effect
        if (isDamaged)
        {
            damageFlashImage.color = flashColor;
        }
        else
        {
            damageFlashImage.color = Color.Lerp(damageFlashImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        isDamaged = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        targetHealth = currentHealth; // Set the target value for smooth animation

        if (healthText != null)
        {
            healthText.text = $"{currentHealth}/{maxHealth}";
        }

        isDamaged = true;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player has died!");

        if (deathScreen != null)
        {
            deathScreen.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
