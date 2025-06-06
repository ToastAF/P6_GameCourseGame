using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : PlayerParent
{
    public SequencerHandler1 sequencerHandler1;
    public PickUpNotifier pickUpNotifier;

    public GameObject attackDirection;
    public AudioClip dash;
    public soundAttack dashSound;

    Animator animator;
    public GameObject animObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = animObject.GetComponent<Animator>();
    }

    void Update()
    {
        MovePlayer();


        if (rb.linearVelocity.magnitude > 0.6f)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        if (rightStickVector.magnitude >= 0.5f)
        {
            LookWherePoint();
        }

        if (movementVector.magnitude >= 0.5f)
        {
            LookWhereGo();
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
