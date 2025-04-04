using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Poo : PlayerParent
{
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovePlayer();
    }

    public void OnMovePlayer2(InputValue input)
    {
        movementVector = input.Get<Vector2>();
    }

    public void OnJumpPlayer2(InputValue input)
    {
        Jump();
    }
}
