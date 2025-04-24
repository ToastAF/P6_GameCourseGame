using UnityEngine;
using UnityEngine.InputSystem;

public class Knockout : MonoBehaviour
{
    public GameObject player1, player2;
    public GameOver gameOverScript;

    public GameObject outerRing;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            EliminatePlayer(other.gameObject);
            gameOverScript.player1Won = true;
        }
        if (other.gameObject.CompareTag("Player2"))
        {
            EliminatePlayer(other.gameObject);
            gameOverScript.player1Won = false;
        }
    }

    void EliminatePlayer(GameObject player)
    {
        Debug.Log("Eliminated Player");

        TurnPlayersOff();
        gameOverScript.gameEnded = true;

        //player.GetComponent<PlayerInput>().enabled = false;
        //player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
    

    void TurnPlayersOff()
    {
        player1.GetComponent<PlayerInput>().enabled = false;
        player1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player2.GetComponent<PlayerInput>().enabled = false;
        player2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
