using UnityEngine;

public class PulsatingSpotlightController : MonoBehaviour
{
    public float colorChangeSpeed = 1.0f;
    public float minBrightness = 0.2f; // Minimum brightness to prevent dark colors

    [Header("Child References")]
    public Light spotlight;             // Assign the Light component here
    public Renderer lampRenderer;       // Assign the lamp mesh's Renderer here
    public string lampColorProperty = "_BaseColor"; // Default for URP

    private Color currentColor;
    private Color targetColor;
    private float t;

    void Start()
    {
        if (spotlight == null || lampRenderer == null)
        {
            Debug.LogError("Spotlight or Lamp Renderer not assigned in the inspector.");
            enabled = false;
            return;
        }

        currentColor = GenerateBrightColor();
        targetColor = GenerateBrightColor();
        spotlight.color = currentColor;
        UpdateLampMaterialColor(currentColor);
        t = 0f;
    }

    void Update()
    {
        t += Time.deltaTime * colorChangeSpeed;

        Color newColor = Color.Lerp(currentColor, targetColor, t);
        spotlight.color = newColor;
        UpdateLampMaterialColor(newColor);

        if (t >= 1f)
        {
            currentColor = targetColor;
            targetColor = GenerateBrightColor(); // Ensure new color is bright
            t = 0f;
        }
    }

    void UpdateLampMaterialColor(Color color)
    {
        if (lampRenderer.material.HasProperty(lampColorProperty))
        {
            lampRenderer.material.SetColor(lampColorProperty, color);
        }
        else if (lampRenderer.material.HasProperty("_EmissionColor"))
        {
            lampRenderer.material.EnableKeyword("_EMISSION"); // Enable emission if it has that property
            lampRenderer.material.SetColor("_EmissionColor", color);
        }
    }

    // Generate a random color but with a minimum brightness
    Color GenerateBrightColor()
    {
        // Random ColorHSV with min saturation and value to avoid dark colors
        float hue = Random.Range(0f, 1f);
        float saturation = Random.Range(0.5f, 1f); // Set minimum saturation to avoid dull colors
        float value = Random.Range(minBrightness, 1f); // Set the minimum brightness (lightness)

        return Color.HSVToRGB(hue, saturation, value);
    }
}
