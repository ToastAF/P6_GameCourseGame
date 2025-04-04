using System;
using UnityEngine;
using TMPro;

public class HealthSystem : MonoBehaviour
{

    public int playerDamageAmount;
    public Rigidbody rb;
    public float forceAmount = 10f;
    public TextMeshProUGUI playerDamageText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerDamageAmount = 0;
    }
    
    
    void addDamage(int damage)
    {
        playerDamageAmount+=10;
    }

    private void Update()
    {
        
        playerDamageText.text = "Player Damage: " + playerDamageAmount;
        float knockbackModifier = playerDamageAmount * 0.5f;
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            addDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            rb.AddForce(transform.forward * (forceAmount+knockbackModifier), ForceMode.Impulse);
        }
    }
    
}
