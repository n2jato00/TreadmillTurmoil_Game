using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public GameObject soundOnPanel;
    public GameObject soundOffPanel;
    public GameObject musicOnPanel;
    public GameObject musicOffPanel;

    private bool soundEnabled = true;
    private bool musicEnabled = true;

    private string soundPrefsKey = "SoundEnabled";
    private string musicPrefsKey = "MusicEnabled";



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {


        // Lataa tallennetut tilat
        LoadPlayerPrefs();

        // P‰ivit‰ paneelien tilat
        UpdateSoundPanel();
        UpdateMusicPanel();

        // Aloita musiikin toisto vain jos se oli p‰‰ll‰ edellisell‰ scenell‰
        if (musicEnabled)
        {
            musicSource.Play();
        }
    }


    public void MusicOn()
    {
        musicEnabled = true;
        UpdateMusicPanel();
        // Tarkista onko scene vaihdettu, ja aloita musiikin toisto vasta sitten
        musicSource.Play();
        SavePlayerPrefs();
    }

    public void MusicOff()
    {
        musicEnabled = false;
        UpdateMusicPanel();
        musicSource.Pause();
        SavePlayerPrefs();
    }

    public void SoundOn()
    {
        soundEnabled = true;
        UpdateSoundPanel();
        SoundManager.sound = true;
        SavePlayerPrefs();
    }

    public void SoundOff()
    {
        soundEnabled = false;
        UpdateSoundPanel();
        SoundManager.sound = false;
        SavePlayerPrefs();

    }


    private void UpdateSoundPanel()
    {
        soundOnPanel.SetActive(soundEnabled);
        soundOffPanel.SetActive(!soundEnabled);
    }

    private void UpdateMusicPanel()
    {
        musicOnPanel.SetActive(musicEnabled);
        musicOffPanel.SetActive(!musicEnabled);
    }


    private void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt(soundPrefsKey, soundEnabled ? 1 : 0);
        PlayerPrefs.SetInt(musicPrefsKey, musicEnabled ? 1 : 0);
        PlayerPrefs.SetInt("SoundManagerSound", SoundManager.sound ? 1 : 0); // Tallenna SoundManager.sound
        PlayerPrefs.Save();
    }


    private void LoadPlayerPrefs()
    {
        soundEnabled = PlayerPrefs.GetInt(soundPrefsKey, 1) == 1;
        musicEnabled = PlayerPrefs.GetInt(musicPrefsKey, 1) == 1;

        // Lataa SoundManager.sound PlayerPrefist‰
        SoundManager.sound = PlayerPrefs.GetInt("SoundManagerSound", 1) == 1;
    }

}
