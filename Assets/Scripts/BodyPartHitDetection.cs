using UnityEngine;

public class BodyPartHitDetection : MonoBehaviour
{
    public delegate void BodyPartHit(string bodyPart, Rigidbody rb);

    public static event BodyPartHit OnBodyPartHit;

    public float cooldownTime = 0.2f; // Aika sekunneissa ennen kuin uusi osuma voidaan ottaa vastaan.
    private bool canHit = true;

    void OnCollisionEnter(Collision collision)
    {
        if (canHit && OnBodyPartHit != null)
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                OnBodyPartHit(gameObject.tag, rb);
            }
            canHit = false;
            Invoke("ResetHit", cooldownTime);
        }
    }


    // Metodi, joka sallii osumien vastaanoton.
    void ResetHit()
    {
        canHit = true;
    }
}
