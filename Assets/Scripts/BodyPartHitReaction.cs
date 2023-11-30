using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartHitReaction : MonoBehaviour
{
    [Tooltip("Movement speed when the character is not hit.")]
    public float forwardSpeed = 0.2f;

    [Tooltip("How fast the character moves backward after being hit.")]
    private float hitReactionSpeed = 2.3f;

    [Tooltip("How long the character moves backward after being hit.")]
    public float hitReactionDuration = 0.5f;

    [Tooltip("Minimum Z-axis value for the character (front).")]
    private float minZValue = -2.9f; // Min value on front

    [Tooltip("Maximum Z-axis value for the character (back).")]
    private float maxZValue = -0.9f; // Max value on back

    private float xValue = 0.46f;

    private float timeSinceLastSpeedIncrease = 0f;
    private float speedIncreaseInterval = 1f; // How often speed increase in seconds
    private float speedIncreaseAmount = 0.01f; // How much speed increase
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
                hitReactionSpeed = 0f;
                transform.position = new Vector3(xValue, transform.position.y, minZValue);
                isOutsideLimits = true;
            }
            else
            {
               
                isOutsideLimits = false;
                timeOutsideLimits = 0f;

                // increase speed after speedIncreaseInterval
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

    public void HandleBodyPartHit(string bodyPart, Rigidbody rb)
    {
        isHit = true;
        hitReactionEndTime = Time.time + hitReactionDuration;

       
        if (bodyPart == "Head" && rb.gameObject.tag != "Test")
        {
         
            hitReactionSpeed = -2.0f;
            StartCoroutine(GameOverDelay(0.8f));
        }
        else if (bodyPart != "Leg" && rb.gameObject.tag != "Test")
        {
            hitReactionSpeed = 0f;
        }
        else
        {

            hitReactionSpeed = 2.3f;
        }
    }
    private IEnumerator GameOverDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Set the "Sprint" parameter to false after the delay
        gameOver.EndGame();
    }
}
