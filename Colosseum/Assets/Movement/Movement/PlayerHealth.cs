using UnityEngine;
using UnityEngine.UI; // Required to interact with the UI components
using UnityEngine.SceneManagement; // For restarting the game

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    // Damage flash settings
    public Image damageFlashImage; // Reference to the flash image
    public float flashSpeed = 5f; // Speed of flash fade-out
    public Color flashColor = new Color(1f, 0f, 0f, 0.5f); // Red, semi-transparent
    private bool isDamaged = false;

    // Death screen settings
    public GameObject deathScreen; // Reference to the death screen panel

    void Start()
    {
        currentHealth = maxHealth;

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
        Debug.Log("Player health: " + currentHealth);

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

        // Optionally disable player controls here (depends on your game setup)
        // Destroy(gameObject); // Optionally destroy the player, but usually you'll want to just disable the player character's controls
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
