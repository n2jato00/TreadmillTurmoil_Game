using UnityEngine;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour
{
    public Text highscoreText;
    public Text currentscoreText;

    private int highscore = 0;
    private int currentscore = 0;

    void Start()
    {
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        UpdateHighscoreText();
    }

    void UpdateHighscoreText()
    {
        highscoreText.text = highscore.ToString();
    }

    void UpdateCurrentScore()
    {
        currentscoreText.text = currentscore.ToString();
    }

    public void UpdateHighscore(int newScore)
    {
        currentscore = newScore;
        UpdateCurrentScore();
        if (newScore > highscore)
        {
            highscore = newScore;
            PlayerPrefs.SetInt("Highscore", highscore);
            PlayerPrefs.Save();
            UpdateHighscoreText();
        }
    }

}
