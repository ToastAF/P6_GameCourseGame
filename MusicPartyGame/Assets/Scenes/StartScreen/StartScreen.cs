using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class CountdownController : MonoBehaviour
{
    public Button startButton;       // Reference to the start button
    public TMP_Text countdownText;   // Reference to the text that shows the countdown

    private void Start()
    {
        // Ensure the game is paused when the scene starts
        Time.timeScale = 0f;

        // Initially hide the countdown text and the start button
        countdownText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);

        // Add listener to the start button to begin countdown when pressed
        startButton.onClick.AddListener(StartCountdown);
    }

    void StartCountdown()
    {
        // Disable the start button to prevent further clicks
        startButton.gameObject.SetActive(false);

        // Show the countdown text
        countdownText.gameObject.SetActive(true);

        // Start the countdown coroutine
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

        // Start the game (replace this with your game start logic)
        StartGame();
    }

    void StartGame()
    {
        // Logic to start the game goes here
        Debug.Log("Game Started!");
        // Example: Load a new scene, enable gameplay scripts, etc.
    }
}
