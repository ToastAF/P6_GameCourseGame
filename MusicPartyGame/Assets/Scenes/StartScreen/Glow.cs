using UnityEngine;
using UnityEngine.UI;

public class NeonFlicker : MonoBehaviour
{
    public RawImage image;
    public Color glowColor = Color.cyan;
    public float minFlickerInterval = 0.05f;
    public float maxFlickerInterval = 0.3f;

    private Color originalColor;
    private float nextFlickerTime;
    private float timer;
    Color dimmedColor = new Color(0.1f, 0.3f, 0.3f); 

    void Start()
    {
        originalColor = image.color;
        ScheduleNextFlicker();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextFlickerTime)
        {
            // Randomly choose whether to flicker on or off
            bool isOn = Random.value > 0.4f;

            image.color = isOn ? glowColor : dimmedColor;

            ScheduleNextFlicker();
        }
    }

    void ScheduleNextFlicker()
    {
        timer = 0f;
        nextFlickerTime = Random.Range(minFlickerInterval, maxFlickerInterval);
    }
}