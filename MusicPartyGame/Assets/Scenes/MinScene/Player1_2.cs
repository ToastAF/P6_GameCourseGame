using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1_2 : MonoBehaviour
{
    public float moveSpeed;

    Rigidbody rb;

    Vector2 movementVector;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.AddForce(new Vector3(movementVector.x * moveSpeed * Time.deltaTime, 0, movementVector.y * moveSpeed * Time.deltaTime), ForceMode.Impulse); // Move Player
    }

    public void OnMove(InputValue input)
    {
        movementVector = input.Get<Vector2>();
        //transform.position += new Vector3(movementVector.x, 0, movementVector.y);
    }
}
