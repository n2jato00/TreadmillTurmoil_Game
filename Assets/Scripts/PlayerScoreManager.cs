using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text multiplierText; // Reference to the UI Text component for the multiplier.
    public Transform playerTransform;

    public float zMultiplierStart = 0;
    public float zMultiplierEnd = 10;
    public float maxMultiplier = 2f;

    private int score = 0;

    void OnEnable()
    {
        BodyPartHitDetection.OnBodyPartHit += UpdateScore;
    }

    void OnDisable()
    {
        BodyPartHitDetection.OnBodyPartHit -= UpdateScore;
    }

    void Start()
    {
        UpdateUIText();
    }

    void Update()
    {
        UpdateMultiplierText();
    }

    void UpdateScore(string bodyPart, Rigidbody rb)
    {
        int zMultiplier = CalculateZMultiplier();

        if (bodyPart == "Head")
        {
            score += 10 * zMultiplier;
        }
        else if (bodyPart == "Torso")
        {
            score += 5 * zMultiplier;
        }
        else if (bodyPart == "Arm")
        {
            score += 3 * zMultiplier;
        }
        else if (bodyPart == "Leg")
        {
            score += 2 * zMultiplier;
        }

        UpdateUIText();
    }

    int CalculateZMultiplier()
    {
        float normalizedPosition = Mathf.InverseLerp(zMultiplierStart, zMultiplierEnd, playerTransform.position.z);
        return Mathf.RoundToInt(1 + normalizedPosition * (maxMultiplier - 1));
    }

    void UpdateUIText()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateMultiplierText()
    {
        int zMultiplier = CalculateZMultiplier();
        multiplierText.text = "x" + zMultiplier.ToString();
    }
}
