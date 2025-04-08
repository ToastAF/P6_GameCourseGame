using System;
using UnityEngine;
using TMPro;

public class DamageSystem : MonoBehaviour
{

    public int playerDamageAmount;
    public Rigidbody rb;
    public float forceAmount = 10f;
    public float knockbackModifier;
    public TextMeshProUGUI playerDamageText;
    

    void Start()
    {
        playerDamageAmount = 0;
        rb = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        playerDamageText.text = "Player Damage: " + playerDamageAmount;
        knockbackModifier = playerDamageAmount * 0.5f;
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            AddDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            rb.AddForce(transform.forward * (forceAmount+knockbackModifier), ForceMode.Impulse);
        }
    }

    void AddDamage(int damage)
    {
        playerDamageAmount += damage;
    }

    public void KnockBack(GameObject other, int damage)
    {
        AddDamage(damage); // Tilf�j skade f�r man bliver skudt ad h til

        // Retning hen mod projektilet. Under skriver vi minus, fordi man skal flyve den anden retning
        Vector3 directionVector = new Vector3(other.transform.position.x, 0, other.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z);
        rb.AddForce(-directionVector * (forceAmount + knockbackModifier), ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack1"))
        {
            KnockBack(other.gameObject, 10);
        }

        if (other.gameObject.CompareTag("Attack1"))
        {
            KnockBack(other.gameObject, 20);
        }

        if (other.gameObject.CompareTag("Attack1"))
        {
            KnockBack(other.gameObject, 30);
        }
    }

}
