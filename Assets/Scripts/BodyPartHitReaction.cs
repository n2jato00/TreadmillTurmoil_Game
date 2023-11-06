using UnityEngine;

public class BodyPartHitReaction : MonoBehaviour
{
    [Tooltip("Movement speed when the character is not hit.")]
    public float forwardSpeed = 1f;

    [Tooltip("How fast the character moves backward after being hit.")]
    public float hitReactionSpeed = 2f;

    [Tooltip("How long the character moves backward after being hit.")]
    public float hitReactionDuration = 0.5f;

    [Tooltip("Minimum Z-axis value for the character (front).")]
    public float minZValue = -3f; // Aseta minimiarvo eteenpäin

    [Tooltip("Maximum Z-axis value for the character (back).")]
    public float maxZValue = -0.5f; // Aseta maksimiarvo taaksepäin

    public float timeBeforeGameEnds = 5f;
    private float hitReactionEndTime;
    private bool isHit = false;
    public GameOver gameOver;

    private bool isOutsideLimits = false;
    private float timeOutsideLimits = 0f;

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

            if (transform.position.z <= minZValue)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, minZValue);
                isOutsideLimits = true;
            }
            else
            {
                isOutsideLimits = false;
                timeOutsideLimits = 0f;
                // Move the character forward along the Z-axis.
                transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
            }

            // Check if the character has been beyond minZValue for 5 seconds and call EndGame().
            if (isOutsideLimits)
            {
                timeOutsideLimits += Time.deltaTime;
                if (timeOutsideLimits >= timeBeforeGameEnds)
                {
                    gameOver.EndGame();
                }
            }

            if (transform.position.z > maxZValue)
            {
                gameOver.EndGame();
            }
        }
    }

    private void HandleBodyPartHit(string bodyPart, Rigidbody rb)
    {
        isHit = true;
        hitReactionEndTime = Time.time + hitReactionDuration;
    }
}
