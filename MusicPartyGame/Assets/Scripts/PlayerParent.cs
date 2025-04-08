using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParent : MonoBehaviour
{
    [Header("Health")]
    public float currentHealth, maxHealth;
    public RectTransform healthBarUI;

    [Header("Movement")]
    public float moveSpeed, jumpForce, rotateSpeed;

    public float xForce, zForce;

    public Rigidbody rb;

    public Vector2 movementVector;
    public Vector3 facingVector;

    public bool canJump;

    public GameObject testAttack;


    //Initierende metoder :D

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {

    }

    //Funktionerne

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    public void MovePlayer() // Den her bliver kaldt på Player1 og Player2 i deres Update() ift deres respektive OnMove() funktioner
    {
        rb.AddForce(new Vector3(movementVector.x * moveSpeed * Time.deltaTime, 0, movementVector.y * moveSpeed * Time.deltaTime), ForceMode.Impulse); // Move Player
    }

    public void LookWhereGo()
    {
        xForce = rb.linearVelocity.x;
        zForce = rb.linearVelocity.z;

        facingVector = new Vector3(movementVector.x, 0, movementVector.y);
        facingVector.Normalize();

        if(facingVector.magnitude >= 0.5f)
        {
            Quaternion toRotation = Quaternion.LookRotation(facingVector, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
    }

    public void Jump() // Den her bliver kaldt på Player1 og Player2 i deres respektive OnJump() funktioner
    {
        if(canJump == true)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            canJump = false;
        }
    }
}
