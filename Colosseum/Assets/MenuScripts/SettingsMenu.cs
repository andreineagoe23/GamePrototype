using TMPro; // Import TextMeshPro namespace
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider sensitivitySlider; // Reference to the slider
    public TMP_Text sensitivityValueText; // Reference to the Text element
    public static float sensitivity = 50f; // Default sensitivity value
    public GameObject settingsMenu;
    public GameObject pauseMenu;

    private void Start()
    {
        // Initialize the slider value
        sensitivitySlider.value = sensitivity;
        sensitivityValueText.text = sensitivity.ToString("F1"); // Display the initial value

        // Add a listener to update sensitivity when the slider value changes
        sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);
    }

    public void UpdateSensitivity(float value)
    {
        sensitivity = value; // Update the sensitivity value
        sensitivityValueText.text = value.ToString("F1"); // Update the displayed value
        Debug.Log("Sensitivity set to: " + sensitivity);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false); // Hide the Settings Menu
        pauseMenu.SetActive(true); // Show the Pause Menu
        GameManager.Instance.SetGameState(GameManager.GameState.Paused); // Set game state to Paused
    }
}
