using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game1");
    }
    public void LoadStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }

}
