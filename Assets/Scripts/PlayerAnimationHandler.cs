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

        if (objectMass < 5.0f)
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

        animator.SetTrigger(triggerName);
    }

}
