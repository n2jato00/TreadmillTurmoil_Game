using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource hitSound; // Reference to the AudioSource for the hit sound
    public AudioSource screamSound;
    public AudioSource ouchSound;
    public AudioSource runningSound;
    public AudioSource headShot;
    public static bool sound = true;

    private void OnEnable()
    {
        BodyPartHitDetection.OnBodyPartHit += HandleBodyPartHit;
    }

    private void OnDisable()
    {
        BodyPartHitDetection.OnBodyPartHit -= HandleBodyPartHit;
    }
    private void Awake()
    {
        if (sound == true)
        {
            PlayRunningSound();
        }
       
    }
    void HandleBodyPartHit(string bodyPart, Rigidbody rb)
    {
        // T�ss� voit toistaa ��ni� riippuen ruumiinosasta
        if (bodyPart == "Head" && rb.gameObject.tag == "Banana")
        { 
            PlayHeadShot();
        }
        else if (bodyPart != "Leg")
        {
            PlayHitSound();
            PlayOuchSound();
        }
        else if (bodyPart == "Leg")
        {
            PlayScreamSound();
            PlayHitSound();
        }


    }
    void PlayOuchSound()
    {
        if (ouchSound != null && sound == true)
        {
            ouchSound.Play();
        }
    }
    void PlayHitSound()
    {
        if (hitSound != null && sound == true)
        {
            hitSound.Play();
        }
    }

    void PlayScreamSound()
    {
        if (screamSound != null && sound == true)
        {
            screamSound.Play();
        }
    }

    void PlayRunningSound()
    {
        if (runningSound != null && sound == true)
        {
            runningSound.Play();
        }
    }

     void PlayHeadShot()
    {
        if (hitSound != null && sound == true)
        {
            headShot.Play();
        }
    }

}
