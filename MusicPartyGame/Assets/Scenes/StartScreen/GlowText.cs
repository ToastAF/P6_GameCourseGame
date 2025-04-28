using UnityEngine;
using TMPro;

public class GlowText : MonoBehaviour
{
    public TMP_Text text; // TextMeshProUGUI or TMP_Text works
    public Color glowColor = Color.cyan;
    public float minFlickerInterval = 0.05f;
    public float maxFlickerInterval = 0.3f;

    private Color originalColor;
    private float nextFlickerTime;
    private float timer;
    private Color dimmedColor = new Color(0.1f, 0.3f, 0.3f);

    void Start()
    {
        originalColor = text.color;
        ScheduleNextFlicker();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextFlickerTime)
        {
            bool isOn = Random.value > 0.4f;

            text.color = isOn ? glowColor : dimmedColor;

            ScheduleNextFlicker();
        }
    }

    void ScheduleNextFlicker()
    {
        timer = 0f;
        nextFlickerTime = Random.Range(minFlickerInterval, maxFlickerInterval);
    }
}
