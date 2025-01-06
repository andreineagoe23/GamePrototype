using UnityEngine;
using UnityEngine.UI; // Required to interact with the UI components
using UnityEngine.SceneManagement; // For restarting the game
using TMPro; // Required for TextMeshPro

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    // UI References
    public Slider healthBar; // Reference to the health bar slider
    public TextMeshProUGUI healthText; // Reference to the health text
    public Image damageFlashImage; // Reference to the flash image
    public GameObject deathScreen; // Reference to the death screen panel

    // Damage flash settings
    public float flashSpeed = 5f; // Speed of flash fade-out
    public Color flashColor = new Color(1f, 0f, 0f, 0.5f); // Red, semi-transparent
    private bool isDamaged = false;

    void Start()
    {
        currentHealth = maxHealth;

        // Initialize the health bar
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        // Initialize the health text
        UpdateHealthText();

        // Hide the death screen at the start
        if (deathScreen != null)
        {
            deathScreen.SetActive(false);
        }
    }

    void Update()
    {
        // Handle the damage flash effect
        if (isDamaged)
        {
            damageFlashImage.color = flashColor;
        }
        else
        {
            // Fade the flash image back to transparent
            damageFlashImage.color = Color.Lerp(damageFlashImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damage flag
        isDamaged = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't drop below 0

        Debug.Log("Player health: " + currentHealth);

        // Update the health bar and health text
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
        UpdateHealthText();

        // Trigger the damage effect
        isDamaged = true;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player has died!");

        // Show the death screen when the player dies
        if (deathScreen != null)
        {
            deathScreen.SetActive(true);
        }

        // Optionally disable player controls here
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth + "  ";
        }
    }

    // Method to restart the game (link to Restart button in the UI)
    public void RestartGame()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Method to quit the game (link to Quit button in the UI)
    public void QuitGame()
    {
        Application.Quit();
    }
}
