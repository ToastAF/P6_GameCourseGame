using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SynthAttack2 : MonoBehaviour
{
    public SequencerHandler1 handler;
    public GameObject player;
    Player2 playerScript;

    Rigidbody rb;
    Vector3 target;
    public float speed;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerScript = player.GetComponent<Player2>();

        target = playerScript.attackDirection.transform.position; // Vi finder den anden player
        transform.LookAt(target);

        Vector3 directionForce = target - new Vector3(transform.position.x, 0, transform.position.z);
        rb.AddForce(Vector3.Normalize(directionForce) * speed, ForceMode.Impulse);
    }
}
