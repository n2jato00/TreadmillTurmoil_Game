using UnityEngine;

public class BodyPartHitReaction : MonoBehaviour
{
    [Tooltip("Liikkumisnopeus kun hahmoa ei ole osuttu.")]
    public float forwardSpeed = 1f;

    [Tooltip("Kuinka nopeasti hahmo liikkuu taaksepäin osuman jälkeen.")]
    public float hitReactionSpeed = 2f;

    [Tooltip("Kuinka kauan hahmo liikkuu taaksepäin osuman jälkeen.")]
    public float hitReactionDuration = 0.5f;
    [Tooltip("Hahmon maksimi Z-arvo.")]
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
            // Liikuta hahmoa Z-akselin suunnassa taaksepäin.
            transform.Translate(Vector3.back * hitReactionSpeed * Time.deltaTime);
        }
        else
        {
            isHit = false;

            // Jos hahmo on lähellä tai yli maksimi Z-arvon, aseta sen sijainti maksimiin.
            if (transform.position.z <= maxZValue)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maxZValue);
            }
            else
            {
                // Liikuta hahmoa Z-akselin suunnassa eteenpäin.
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