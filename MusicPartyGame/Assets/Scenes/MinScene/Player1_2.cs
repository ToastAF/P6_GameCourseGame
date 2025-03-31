using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1_2 : MonoBehaviour
{
    public float moveSpeed;

    Rigidbody rb;

    Vector2 movementVector;

    public SequencerHandler1 sequencerHandler1;
    public PickUpNotifier pickUpNotifier;

    private void OnTriggerEnter(Collider other)
    {
        PickUpNotifier notifier = other.GetComponent<PickUpNotifier>();

        if (other.CompareTag("TopPick"))
        {
            sequencerHandler1.pickUpPickedUp(0);
            notifier?.OnCollected();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("MidPick"))
        {
            sequencerHandler1.pickUpPickedUp(1);
            notifier?.OnCollected();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("BotPick"))
        {
            sequencerHandler1.pickUpPickedUp(2);
            notifier?.OnCollected();
            Destroy(other.gameObject);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        rb.AddForce(new Vector3(movementVector.x * moveSpeed * Time.deltaTime, 0, movementVector.y * moveSpeed * Time.deltaTime), ForceMode.Impulse); // Move Player
    }

    public void OnMovePlayer1(InputValue input)
    {
        movementVector = input.Get<Vector2>();
        //transform.position += new Vector3(movementVector.x, 0, movementVector.y);
    }
}
