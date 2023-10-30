using UnityEngine;

public class BodyPartHitDetection : MonoBehaviour
{
    public delegate void BodyPartHit(string bodyPart);
    public static event BodyPartHit OnBodyPartHit;

    public float cooldownTime = 0.2f; // Aika sekunneissa ennen kuin uusi osuma voidaan ottaa vastaan.
    private bool canHit = true;

    void OnCollisionEnter(Collision collision)
    {
        if (canHit && OnBodyPartHit != null)
        {
            OnBodyPartHit(gameObject.tag);
            canHit = false; // Estet‰‰n osumien vastaanotto.
            Invoke("ResetHit", cooldownTime); // Kutsuu ResetHit-metodia cooldownTime sekunnin kuluttua.
        }
    }

    // Metodi, joka sallii osumien vastaanoton.
    void ResetHit()
    {
        canHit = true;
    }
}
