using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;

  
    void Start()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        // Lis‰‰ kuuntelijat nappuloille.

    }

    public void EndGame()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        // Lataa uudelleenk‰ynnistyksen yhteydess‰ "Game1" -kohtaus.
        Time.timeScale = 1;
        SceneManager.LoadScene("Game1");
       

    }

    public void GoMainMenu()
    {
        // Lataa p‰‰valikko "StartScreen" -kohtaus.
        SceneManager.LoadScene("StartScreen");
    }
}
