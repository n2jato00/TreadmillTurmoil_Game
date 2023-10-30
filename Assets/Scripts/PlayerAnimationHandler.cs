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

    void PlayHitAnimation(string bodyPart)
    {
        if (bodyPart == "Head")
        {
            animator.SetTrigger("HeadHit");
        }
        else if (bodyPart == "Torso")
        {
            animator.SetTrigger("TorsoHit");
        }
        else if (bodyPart == "Arm")
        {
            animator.SetTrigger("ArmHit");
        }
        else if (bodyPart == "Leg")
        {
            animator.SetTrigger("LegHit");
        }
    }

}
