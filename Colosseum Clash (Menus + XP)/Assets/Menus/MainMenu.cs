using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Anush"); // Replace "GameScene" with your game scene name
    }

    public void OpenControls()
    {
        Debug.Log("Controls Menu Clicked");
        // Add code to open the controls menu or load a scene
    }

    public void OpenSettings()
    {
        Debug.Log("Settings Menu Clicked");
        // Add code to open the settings menu or load a scene
    }
}
