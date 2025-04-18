using UnityEngine;
using UnityEngine.InputSystem;

public class Knockout : MonoBehaviour
{

    public GameObject outerRing;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            EliminatePlayer(other.gameObject);
        }
        if (other.gameObject.tag == "Player2")
        {
            EliminatePlayer(other.gameObject);
        }
    }

    void EliminatePlayer(GameObject player)
    {
        Debug.Log("Eliminated Player");
        player.GetComponent<PlayerInput>().enabled = false;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
    
}
