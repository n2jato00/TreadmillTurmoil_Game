using UnityEngine;

public class BodyPartHitReaction : MonoBehaviour
{
    [Tooltip("Movement speed when the character is not hit.")]
    public float forwardSpeed = 1f;

    [Tooltip("How fast the character moves backward after being hit.")]
    public float hitReactionSpeed = 2f;

    [Tooltip("How long the character moves backward after being hit.")]
    public float hitReactionDuration = 0.5f;

    [Tooltip("Maximum Z-axis value for the character.")]
    public float maxZValue = 10f;

    private float hitReactionEndTime;
    private bool isHit = false;

    private void OnEnable()
    {
        BodyPartHitDetection.OnBodyPartHit += HandleBodyPartHit;
    }

    private void OnDisable()
    {
        BodyPartHitDetection.OnBodyPartHit -= HandleBodyPartHit;
    }

    private void Update()
    {
        if (isHit && Time.time < hitReactionEndTime)
        {
            // Move the character backward along the Z-axis.
            transform.Translate(Vector3.back * hitReactionSpeed * Time.deltaTime);
        }
        else
        {
            isHit = false;

            // If the character is near or beyond the maximum Z-value, set its position to the maximum.
            if (transform.position.z <= maxZValue)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maxZValue);
            }
            else
            {
                // Move the character forward along the Z-axis.
                transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
            }
        }
    }

    private void HandleBodyPartHit(string bodyPart)
    {
        isHit = true;
        hitReactionEndTime = Time.time + hitReactionDuration;
    }
}
