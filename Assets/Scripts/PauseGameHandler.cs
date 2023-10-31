using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameHandler : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseManager.canThrow = false; // Estä heittäminen tauon aikana
    }
    public void ResumeGame()
    {
        StartCoroutine(ResumeGameWithDelay());
    }

    private IEnumerator ResumeGameWithDelay()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.5f);
        PauseManager.canThrow = true;
    }
}

