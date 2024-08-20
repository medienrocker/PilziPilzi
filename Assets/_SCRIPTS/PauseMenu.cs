using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true); // Show the Pause Menu
        Time.timeScale = 0f; // Freeze the game
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // Hide the Pause Menu
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
    }

    public void RestartCurrentLevel() {
        ResumeGame();
        LevelManager.Instance.RestartLevel();
    }

    // Public function to be called by the Pause Menu button
    public void GoToRoadmap()
    {
        Time.timeScale = 1f; // Ensure the game is unpaused before loading another scene
        LevelManager.Instance.GoToRoadmap(); // Call the GoToRoadmap function in LevelManager
    }

    // Public function to be called by the Pause Menu button
    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Ensure the game is unpaused before loading another scene
        LevelManager.Instance.GoToMainMenu(); // Call the GoToRoadmap function in LevelManager
    }
}
