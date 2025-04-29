using UnityEngine;
using System.Collections;
using TMPro;

public class CountdownStarter : MonoBehaviour
{
    public TMP_Text countdownText;
    SelectingSong bruh;


    void Start()
    {
        bruh = GetComponent<SelectingSong>();

        Time.timeScale = 0f;

        StartCoroutine(CountdownCoroutine());
    }


    IEnumerator CountdownCoroutine()
    {
        int countdownTime = 3; // Start from 3
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();  // Update the text
            yield return new WaitForSecondsRealtime(1);  // Wait for 1 real-time second (ignores Time.timeScale)
            countdownTime--;  // Decrease the countdown
        }

        countdownText.text = "GO!";  // Display "GO!" when countdown is over
        yield return new WaitForSecondsRealtime(1);  // Wait for 1 second before starting the game

        // Hide the UI elements (countdown and start button) after GO!
        countdownText.gameObject.SetActive(false);

        // Resume the game after the "GO!" message
        Time.timeScale = 1f;

        bruh.StartMusic();

        Debug.Log("Game Started!!!");
    }
}
