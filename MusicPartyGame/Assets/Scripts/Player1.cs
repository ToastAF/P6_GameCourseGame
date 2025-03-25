using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1 : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    public void OnMove(InputValue input)
    {
        Vector2 movementVector = input.Get<Vector2>();
        //transform.position += new Vector3(movementVector.x, 0, movementVector.y);
        rb.AddForce(new Vector3(movementVector.x, 0, movementVector.y), ForceMode.Impulse);
    }
}
