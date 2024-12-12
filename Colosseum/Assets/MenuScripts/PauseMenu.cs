using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.CurrentState == GameManager.GameState.Playing)
            {
                PauseGame();
            }
            else if (GameManager.Instance.CurrentState == GameManager.GameState.Paused)
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        GameManager.Instance.SetGameState(GameManager.GameState.Paused);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        GameManager.Instance.SetGameState(GameManager.GameState.Playing);
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        GameManager.Instance.SetGameState(GameManager.GameState.Settings);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        GameManager.Instance.SetGameState(GameManager.GameState.Paused);
    }
}
