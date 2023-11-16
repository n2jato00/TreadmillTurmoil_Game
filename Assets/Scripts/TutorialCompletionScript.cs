using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCompletionScript : MonoBehaviour
{
 
    private bool tutorialCompleted = false;

    private void Start()
    {
        // Check if the tutorial is completed
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
