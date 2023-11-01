using UnityEngine;
using UnityEngine.UI;

public class FramerateDisplay : MonoBehaviour
{
    public Text framerateText;

    private void Update()
    {
        float fps = 1f / Time.unscaledDeltaTime;
        framerateText.text = "FPS: " + fps.ToString("F1");
    }
}
