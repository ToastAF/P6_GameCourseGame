using UnityEngine;
using UnityEngine.UI;

public class PlayerParent : MonoBehaviour
{
    [Header("Health")]
    public float currentHealth, maxHealth;
    public RectTransform healthBarUI;

    [Header("Movement")]
    public float moveSpeed, jumpForce;

    public float xForce, zForce;

    public Rigidbody rb;

    public Vector2 movementVector;

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

    public void Jump() // Den her bliver kaldt på Player1 og Player2 i deres respektive OnJump() funktioner
    {
        if(canJump == true)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            canJump = false;
        }
    }
}
