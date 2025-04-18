using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : PlayerParent
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

        if (facingOverride == true)
        {
            LookWherePoint();
        }
        else
        {
            LookWhereGo();
        }

        if (rightStickVector.magnitude >= 0.5f)
        {
            facingOverride = true;
        }
        else
        {
            facingOverride = false;
        }
    }

    public void OnMovePlayer2(InputValue input)
    {
        movementVector = input.Get<Vector2>();
    }

    public void OnLook(InputValue input)
    {
        rightStickVector = input.Get<Vector2>();
    }

    public void OnJumpPlayer2(InputValue input)
    {
        //Jump();

        Dash(); // Vi dasher i stedet for at jumpe
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
