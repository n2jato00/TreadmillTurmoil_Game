using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartHitDetection : MonoBehaviour
{
    public delegate void BodyPartHit(string bodyPart, Rigidbody rb);
    public static event BodyPartHit OnBodyPartHit;

    private bool canHit = true;

    public AudioSource hitSound; // Reference to the AudioSource for the hit sound
    public AudioSource screamSound;

    // Layer that is intended to be accepted in collisions.
    public LayerMask acceptedLayer;

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
            // Check if the colliding game object's layer is the desired one
            if ((acceptedLayer.value & 1 << collision.gameObject.layer) != 0)
            {
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    OnBodyPartHit(gameObject.tag, rb);
                    PlayHitSound(); // Play the hit sound
                    PlayScreamSound();

                    // Change item layer after hit
                    rb.gameObject.layer = 6;
                }
                canHit = false;
                StartCoroutine(ResetHitDelay(0.25f));
            }
        }
    }

    void PlayHitSound()
    {
        if (hitSound != null)
        {
            hitSound.Play();
        }
    }
    void PlayScreamSound()
    { 
        if (screamSound != null)
        {
            screamSound.Play();
        }
    }
    private IEnumerator ResetHitDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Set the "Sprint" parameter to false after the delay
        canHit = true;
    }

}
