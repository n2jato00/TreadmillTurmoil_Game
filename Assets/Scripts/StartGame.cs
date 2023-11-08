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
        SceneManager.LoadScene("Tutorial");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
