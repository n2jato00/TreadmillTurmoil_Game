using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCompletionScript : MonoBehaviour
{
 
    private bool tutorialCompleted = false;

    private void Start()
    {
        // Tarkista, onko tutorial jo suoritettu
        tutorialCompleted = PlayerPrefs.GetInt("TutorialComplete") == 1;
    }

    public void MarkTutorialComplete()
    {
        if (!tutorialCompleted)
        {
            PlayerPrefs.SetInt("TutorialComplete", 1);
            PlayerPrefs.Save();
            tutorialCompleted = true;
           
        }
    }
}
