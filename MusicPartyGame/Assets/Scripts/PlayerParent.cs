using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParent : MonoBehaviour
{
    public float moveSpeed, jumpForce, rotateSpeed, dashSpeed, dashCoolDown, IFrames;

    public Image dashUI;

    //public float xForce, zForce;

    public Rigidbody rb;

    public Vector2 movementVector;
    public Vector2 rightStickVector;
    public Vector3 facingVector;

    public bool canJump;
    public bool canDash = true;
    public bool isDead;

    public DamageSystem damageSystem;

    public GameObject lookPointer;


    //Funktionerne

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    public void MovePlayer() // Den her bliver kaldt p� Player1 og Player2 i deres Update() ift deres respektive OnMove() funktioner
    {
        rb.AddForce(new Vector3(movementVector.x * moveSpeed * Time.deltaTime, 0, movementVector.y * moveSpeed * Time.deltaTime), ForceMode.Impulse); // Move Player
        
    }

    public void LookWherePoint()
    {
        facingVector = new Vector3(rightStickVector.x, 0, rightStickVector.y);

        if (facingVector.magnitude >= 0.5f)
        {
            lookPointer.transform.rotation = Quaternion.LookRotation(facingVector, Vector3.up);

            //lookPointer.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
    }

    public void LookWhereGo()
    {
        //xForce = rb.linearVelocity.x;
        //zForce = rb.linearVelocity.z;

        facingVector = new Vector3(movementVector.x, 0, movementVector.y);
        facingVector.Normalize();

        if(facingVector.magnitude >= 0.5f)
        {
            Quaternion toRotation = Quaternion.LookRotation(facingVector, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
    }

    public void Jump() // Den her bliver kaldt p� Player1 og Player2 i deres respektive OnJump() funktioner
    {
        if(canJump == true)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            canJump = false;
        }
    }

    public void Dash()
    {
        if(rb.linearVelocity.magnitude > 0.5f && canDash == true)
        {
            facingVector = new Vector3(movementVector.x, 0, movementVector.y);
            facingVector.Normalize();

            rb.AddForce(facingVector * dashSpeed, ForceMode.Impulse);

            GetComponent<AudioSource>().Play();

            StartCoroutine(IFramesCD(IFrames));
            StartCoroutine(DashCD(dashCoolDown));
        }
    }

    IEnumerator IFramesCD(float time)
    {
        damageSystem.canBeHit = false;
        //Debug.Log("I can't be hit!");
        yield return new WaitForSeconds(time);
        damageSystem.canBeHit = true;
        //Debug.Log("Now i can :(");
    }

    IEnumerator DashCD(float time)
    {
        canDash = false;
        dashUI.fillAmount = 0f; // Start empty
        dashUI.color = Color.gray;
        
        float elapsed = 0f;
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            dashUI.fillAmount = Mathf.Clamp01(elapsed / time);
            yield return null;
        }

        canDash = true;
        dashUI.fillAmount = 1f; // Fully filled
        dashUI.color = Color.white;
    }
}
