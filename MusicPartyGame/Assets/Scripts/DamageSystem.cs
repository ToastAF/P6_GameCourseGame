using System;
using UnityEngine;
using TMPro;
using System.Collections;

public class DamageSystem : MonoBehaviour
{
    public int att1Dmg, att2Dmg, att3Dmg;

    public int playerDamageAmount;
    public Rigidbody rb;
    public float forceAmount = 5f;
    public float knockbackModifier;
    public TextMeshProUGUI playerDamageText;

    public bool canBeHit;

    public Animator animator;


    void Start()
    {
        playerDamageAmount = 0;
        rb = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        playerDamageText.text = playerDamageAmount + "%";
        knockbackModifier = playerDamageAmount * 0.2f; //Scaled down knockback to increase fairness



        // FOR TESTING!!!
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
        AddDamage(damage); // Tilføj skade før man bliver skudt ad h til

        // Retning hen mod projektilet. Under skriver vi minus, fordi man skal flyve den anden retning
        Vector3 directionVector = new Vector3(other.transform.position.x, 0, other.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z);
        directionVector.Normalize();
        rb.AddForce(-directionVector * (forceAmount + knockbackModifier), ForceMode.Impulse);

        StartCoroutine(IFramesOnHit(0.2f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(canBeHit == true)
        {
            if (other.gameObject.CompareTag("Attack1"))
            {
                KnockBack(other.gameObject, att1Dmg);
                Debug.Log("Hit by attack 1");
            }

            if (other.gameObject.CompareTag("Attack2"))
            {
                KnockBack(other.gameObject, att2Dmg);
                Debug.Log("Hit by attack 2");
            }

            if (other.gameObject.CompareTag("Attack3"))
            {
                KnockBack(other.gameObject, att3Dmg);
                Debug.Log("Hit by attack 3");
            }
        }
    }

    IEnumerator IFramesOnHit(float time)
    {
        canBeHit = false;
        animator.SetBool("KnockedBack", true);
        yield return new WaitForSeconds(time);
        canBeHit = true;
        animator.SetBool("KnockedBack", false);
    }
}
