using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1Poo : PlayerParent
{
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.F))
        {
            AttackTest();
        }
    }

    public void OnMovePlayer1(InputValue input)
    {
        movementVector = input.Get<Vector2>();
    }

    public void OnJumpPlayer1(InputValue input)
    {
        Jump();
    }
}
