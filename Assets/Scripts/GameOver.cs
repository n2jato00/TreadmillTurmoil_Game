using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    public PauseGameHandler pauseGameHandler; // Lis‰‰ t‰m‰ Unity Inspectorissa

    void Start()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);

        // Stop the background music when the game is over
        if (pauseGameHandler != null && pauseGameHandler.backgroundMusic != null)
        {
            pauseGameHandler.backgroundMusic.Stop();
        }
    }

    public void RestartGame()
    {
        // Reload the "Game1" scene when restarting
        Time.timeScale = 1;
        SceneManager.LoadScene("Game1");
    }

    public void GoMainMenu()
    {
        // Load the main menu "StartScreen" scene
        SceneManager.LoadScene("StartScreen");
    }
}
