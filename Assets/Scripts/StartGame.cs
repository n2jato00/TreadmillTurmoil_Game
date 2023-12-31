using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void LoadGameScene()
    {
        
        if (PlayerPrefs.GetInt("TutorialComplete") == 1)
        {
            SceneManager.LoadScene("StartScreen");
        }
        else
        {
            PauseManager.canThrow = true;
            SceneManager.LoadScene("Game1"); // Move the game, if tutorial is completed
        }
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
    public void ForceLoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
