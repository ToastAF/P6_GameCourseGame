using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1 : PlayerParent
{
    public SequencerHandler1 sequencerHandler1;
    public PickUpNotifier pickUpNotifier;

    public GameObject attackDirection;

    public AudioClip dash;
    public soundAttack dashSound;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovePlayer();

        if(rightStickVector.magnitude >= 0.5f)
        {
            LookWherePoint();
        }

        if(movementVector.magnitude >= 0.5f)
        {
            LookWhereGo();
        }
    }

    public void OnMovePlayer1(InputValue input)
    {
        movementVector = input.Get<Vector2>();
    }

    public void OnLook(InputValue input)
    {
        rightStickVector = input.Get<Vector2>();
    }

    public void OnJumpPlayer1(InputValue input)
    {
        //Jump();

        Dash(); //Vi skifter Jump til Dash
        if (canDash)
        {
            dashSound.PlaySound(dash, rb.transform.position);   
        }
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
