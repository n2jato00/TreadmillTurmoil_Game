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

    }

    public void EndGame()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        // Lataa uudelleenkäynnistyksen yhteydessä "Game1" -kohtaus.
        Time.timeScale = 1;
        SceneManager.LoadScene("Game1");
       

    }

    public void GoMainMenu()
    {
        // Lataa päävalikko "StartScreen" -kohtaus.
        SceneManager.LoadScene("StartScreen");
    }
}
