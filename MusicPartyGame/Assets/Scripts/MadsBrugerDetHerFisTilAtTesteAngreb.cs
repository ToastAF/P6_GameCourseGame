using System;
using UnityEngine;

public class MadsBrugerDetHerFisTilAtTesteAngreb : MonoBehaviour
{

    public GameObject attack1, player;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            attack();
        }
    }

    void attack()
    {
        GameObject temp = Instantiate(attack1, new Vector3(player.transform.position.x, 0, player.transform.position.z), Quaternion.Euler(-90, 0, 0));
    }
}
