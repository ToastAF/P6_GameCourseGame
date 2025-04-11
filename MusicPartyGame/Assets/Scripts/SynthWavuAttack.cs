using System;
using UnityEngine;

public class SynthWavuAttack : MonoBehaviour
{
    public GameObject sAttack3;
    public GameObject player;
    

    public void attack()
    {
        GameObject temp = Instantiate(sAttack3, new Vector3(player.transform.position.x, 0, player.transform.position.z), Quaternion.Euler(-90, 0, 0));
    }

    private void Update()
    {
        // Check if the player is pressing the attack button (e.g., spacebar)
        if (Input.GetKeyDown(KeyCode.P))
        {
            attack();
        }
    }
}


