using UnityEngine;

public class BodyPartHitDetection : MonoBehaviour
{
    public delegate void BodyPartHit(string bodyPart, Rigidbody rb);

    public static event BodyPartHit OnBodyPartHit;

    public float cooldownTime = 0.2f; // Aika sekunneissa ennen kuin uusi osuma voidaan ottaa vastaan.
    private bool canHit = true;

    public AudioSource hitSound; // Reference to the AudioSource for the hit sound

    private void Start()
    {
        // Ensure that the AudioSource is set up in the Unity Inspector
        if (hitSound == null)
        {
            Debug.LogError("Hit sound AudioSource is not assigned!");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (canHit && OnBodyPartHit != null)
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                OnBodyPartHit(gameObject.tag, rb);
                PlayHitSound(); // Play the hit sound
            }
            canHit = false;
            Invoke("ResetHit", cooldownTime);
        }
    }

    void PlayHitSound()
    {
        if (hitSound != null)
        {
            hitSound.Play();
        }
    }

    // Metodi, joka sallii osumien vastaanoton.
    void ResetHit()
    {
        canHit = true;
    }
}
