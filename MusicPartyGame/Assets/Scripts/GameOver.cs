using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public bool gameEnded;

    public bool player1Won; // Den her styrer hvem der vandt

    public GameObject p1WonTxt, p2WonTxt;
    public GameObject restartButton;

    void Start()
    {
        gameEnded = false;

        p1WonTxt.SetActive(false); // Vi slukker for det her UI til at starte med
        p2WonTxt.SetActive(false);
        restartButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameEnded == true)
        {
            restartButton.SetActive(true);
            if(player1Won == true)
            {
                p1WonTxt.SetActive(true);
            }else if(player1Won == false)
            {
                p2WonTxt.SetActive(true);
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Startscreen");
    }
}
