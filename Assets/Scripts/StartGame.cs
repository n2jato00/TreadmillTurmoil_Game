using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void LoadGameScene()
    {
        PauseManager.canThrow = true;
        SceneManager.LoadScene("Game1");
    }
    public void LoadStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }
    public void LoadTutorial()
    {
        if (PlayerPrefs.GetInt("TutorialComplete") == 0)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            PauseManager.canThrow = true;
            SceneManager.LoadScene("Game1"); // Move the game, if tutorial is completed
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
