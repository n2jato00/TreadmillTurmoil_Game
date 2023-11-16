using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameHandler : MonoBehaviour

{

    public AudioSource backgroundMusic; // Reference to your background music AudioSource

    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseManager.canThrow = false; // Stop throws when Pause is activated

        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }
    }
    public void ResumeGame()
    {
        StartCoroutine(ResumeGameWithDelay());
    }

    private IEnumerator ResumeGameWithDelay()
    {
        Time.timeScale = 1;
        backgroundMusic.Play();
        yield return new WaitForSeconds(0.5f);
        PauseManager.canThrow = true;
       
    }
}

