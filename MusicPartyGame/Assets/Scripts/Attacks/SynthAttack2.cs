using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SynthAttack2 : MonoBehaviour
{
    public SequencerHandler1 handler;

    Rigidbody rb;
    Vector3 target;
    public float speed;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        target = GameObject.FindGameObjectWithTag("Player1").transform.position; // Vi finder den anden player
        transform.LookAt(target);

        Vector3 directionForce = target - new Vector3(transform.position.x, 0, transform.position.z);
        rb.AddForce(Vector3.Normalize(directionForce) * speed, ForceMode.Impulse);
    }
}
