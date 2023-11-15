using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource[] sfxSources;

    private bool isSoundMuted = false;

    private void Start()
    {
        // Ensure the initial state of sound matches the toggle state
        SetSoundState(isSoundMuted);
    }

    public void ToggleSound()
    {
        isSoundMuted = !isSoundMuted;
        SetSoundState(isSoundMuted);
    }

    private void SetSoundState(bool isMuted)
    {
        // Toggle music playback based on the state
        if (isMuted)
        {
            musicSource.Pause(); // Pause or stop playing music
        }
        else
        {
            musicSource.Play(); // Start or resume playing music
        }

        // Toggle SFX playback based on the state
        foreach (var sfxSource in sfxSources)
        {
            if (sfxSource != null)
            {
                if (isMuted)
                {
                    sfxSource.Pause(); // Pause or stop playing SFX
                }
                else
                {
                    sfxSource.UnPause(); // Resume playing SFX
                }
            }
        }
    }
}
