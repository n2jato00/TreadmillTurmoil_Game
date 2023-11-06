using UnityEngine;

public class BodyPartHitReaction : MonoBehaviour
{
    [Tooltip("Movement speed when the character is not hit.")]
    public float forwardSpeed = 0.2f;

    [Tooltip("How fast the character moves backward after being hit.")]
    public float hitReactionSpeed = 2.5f;

    [Tooltip("How long the character moves backward after being hit.")]
    public float hitReactionDuration = 0.5f;

    [Tooltip("Minimum Z-axis value for the character (front).")]
    public float minZValue = -3f; // Aseta minimiarvo eteenp‰in

    [Tooltip("Maximum Z-axis value for the character (back).")]
    public float maxZValue = -0.5f; // Aseta maksimiarvo taaksep‰in

    private float timeSinceLastSpeedIncrease = 0f;
    private float speedIncreaseInterval = 1f; // Aika sekunteina, jonka v‰lein nopeus kasvaa.
    private float speedIncreaseAmount = 0.01f; // Kuinka paljon nopeutta lis‰t‰‰n.
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

                // Kasvata nopeutta joka 10 sekunti.
                timeSinceLastSpeedIncrease += Time.deltaTime;
                if (timeSinceLastSpeedIncrease >= speedIncreaseInterval)
                {
                    forwardSpeed += speedIncreaseAmount;
                    timeSinceLastSpeedIncrease = 0f;
                }

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
