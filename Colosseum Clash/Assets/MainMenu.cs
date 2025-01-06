using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game"); // Replace with the name of your game scene
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings"); // Replace with your settings scene or UI logic
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls"); // Replace with your tutorial scene
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit!"); // This works in the editor
        Application.Quit(); // This will close the application when built
    }
}
