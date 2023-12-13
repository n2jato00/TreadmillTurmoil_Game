using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource hitSound; // Reference to the AudioSource for the hit sound
    public AudioSource screamSound;
    public AudioSource ouchSound;
    public AudioSource runningSound;
    public AudioSource headShot;
    public AudioSource dumbbellHead;
    public AudioSource bowlingballHead;
    public AudioSource owwSound;
    public AudioSource chickenSound;
    public AudioSource catSound;

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
        else if (bodyPart == "Head" && rb.gameObject.tag == "Dumbbell")
        {
            Oww();
            Dumbhead();
        }
        else if (bodyPart == "Head" && rb.gameObject.tag == "Bowlingball")
        {
            Oww();
            BallHead();
        }
        else if (rb.gameObject.tag == "Cat")
        {
            PlayHitSound();
            Cat();
        }
        else if (rb.gameObject.tag == "Chicken")
        {
            PlayHitSound();
            Chicken();
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

     void Dumbhead()
    {
        if (hitSound != null && sound == true)
        {
            dumbbellHead.Play();
        }
    }

     void BallHead()
    {
        if (hitSound != null && sound == true)
        {
            bowlingballHead.Play();
        }
    }

     void Oww()
    {
        if (hitSound != null && sound == true)
        {
            owwSound.Play();
        }
    }

     void Cat()
    {
        if (hitSound != null && sound == true)
        {
            catSound.Play();
        }
    }

     void Chicken() {

     
        if (hitSound != null && sound == true)
        {
            chickenSound.Play();
        }
     }
   
 }



