using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1 : PlayerParent
{
    
    
    public SequencerHandler1 sequencerHandler1;
    public PickUpNotifier pickUpNotifier;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovePlayer();

        xForce = rb.linearVelocity.x;
        zForce = rb.linearVelocity.z;
    }

    public void OnMovePlayer1(InputValue input)
    {
        movementVector = input.Get<Vector2>();
    }

    public void OnJumpPlayer1(InputValue input)
    {
        Jump();
    }
    
    // Sequencer stuff down here

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
}
