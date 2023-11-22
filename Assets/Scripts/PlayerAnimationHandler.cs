using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        BodyPartHitDetection.OnBodyPartHit += PlayHitAnimation;
    }

    void OnDisable()
    {
        BodyPartHitDetection.OnBodyPartHit -= PlayHitAnimation;
    }

    void PlayHitAnimation(string bodyPart, Rigidbody rb)
    {
        string triggerName = bodyPart;

        float objectMass = rb.mass;
        string weight;

        if (objectMass < 9.1f)
        {
            weight = "Light";
        }
        else if (objectMass < 11.0f)
        {
            weight = "Normal";
        }
        else
        {
            weight = "Heavy";
        }

        // Check if the tag is "banana"
        if (rb.gameObject.tag == "Banana")
        {
            // Modify triggerName for "Head" and "Leg" when tag is "banaani"
            switch (bodyPart)
            {
                case "Head":
                    triggerName = "HeadLightHit";
                    animator.SetBool("Sprint", true);
                    StartCoroutine(ResetSprintAfterDelay(0.8f));
                    break;
                case "Leg":
                    triggerName = "SlipBanan";
                    break;
                default:
                    triggerName = "TorsoLightHit";
                    break;
            }
        }
        else
        {
            // Default behavior for other tags
            switch (bodyPart)
            {
                case "Head":
                    triggerName += weight + "Hit";
                    break;
                case "Torso":
                    triggerName += weight + "Hit";
                    break;
                case "Arm":
                    triggerName += weight + "Hit";
                    break;
                case "Leg":
                    triggerName += weight + "Hit";
                    break;
                default:
                    triggerName += "NormalHit";
                    break;
            }
        }

        animator.SetTrigger(triggerName);
    }
    private IEnumerator ResetSprintAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Set the "Sprint" parameter to false after the delay
        animator.SetBool("Sprint", false);
    }

}
