using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource;
    private bool isMusicOn = true;

    private void Start()
    {
        // Initialize music state based on the toggle
        SetMusicState(isMusicOn);
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        SetMusicState(isMusicOn);
    }

    private void SetMusicState(bool isMusicOn)
    {
        // Toggle music playback based on the state
        if (isMusicOn)
        {
            musicSource.Play(); // Start or resume playing music
        }
        else
        {
            musicSource.Pause(); // Pause or stop playing music
        }
    }
}
