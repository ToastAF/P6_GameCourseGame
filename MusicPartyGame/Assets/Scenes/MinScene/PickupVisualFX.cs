using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PickupVisualFX : MonoBehaviour
{
    public float floatAmplitude = 0.25f;
    public float floatFrequency = 1f;
    public float rotationSpeed = 30f;
    public float fadeDuration = 1.5f;

    public Color glowColor = Color.yellow;
    public float glowPulseSpeed = 2f;
    public float glowMinIntensity = 0.2f;
    public float glowMaxIntensity = 1f;

    private Vector3 startPos;
    private Renderer rend;
    private Material mat;
    private float fadeTimer;

    void Start()
    {
        startPos = transform.position;
        rend = GetComponent<Renderer>();
        mat = rend.material;

        // Enable emission
        mat.EnableKeyword("_EMISSION");

        // Set initial transparent color
        if (mat.HasProperty("_Color"))
        {
            Color c = mat.color;
            c.a = 0;
            mat.color = c;
        }

        fadeTimer = 0f;
    }

    void Update()
    {
        // Float
        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        // Rotate
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        // Fade in
        if (fadeTimer < fadeDuration)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Clamp01(fadeTimer / fadeDuration);

            if (mat.HasProperty("_Color"))
            {
                Color c = mat.color;
                c.a = alpha;
                mat.color = c;
            }
        }

        // Pulsing glow
        float pulse = Mathf.Lerp(glowMinIntensity, glowMaxIntensity, (Mathf.Sin(Time.time * glowPulseSpeed) + 1f) / 2f);
        Color emission = glowColor * pulse;
        mat.SetColor("_EmissionColor", emission);
    }
}