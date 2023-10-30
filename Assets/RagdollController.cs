using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody[] ragdollParts;

    private void Start()
    {
        // Ota yhteys Animator-komponenttiin
        animator = GetComponent<Animator>();

        // Etsi kaikki Rigidbody-komponentit (ragdollin osat)
        ragdollParts = GetComponentsInChildren<Rigidbody>();

        // Poista aluksi ragdollin vaikutus
        ToggleRagdoll(false);
    }

    private void Update()
    {
        // Tarkista, jos pelaaja painaa välilyöntiä
        if (Input.GetKeyDown("space"))
        {
            ToggleRagdoll(true);
            animator.enabled = false;
        }
    }

        private void ToggleRagdoll(bool state)
    {
        foreach (Rigidbody rb in ragdollParts)
        {
            rb.isKinematic = !state;
        }
    }
}
