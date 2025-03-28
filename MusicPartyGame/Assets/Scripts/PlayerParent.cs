using UnityEngine;

public class PlayerParent : MonoBehaviour
{
    public float moveSpeed, jumpForce;

    public Rigidbody rb;

    public Vector2 movementVector;

    public bool canJump;

    public GameObject testAttack;


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

    public void AttackTest() // Test af attack. Måske de skal sidde på hver deres ting, fordi angrebene er forskellige :)
    {

    }
}
