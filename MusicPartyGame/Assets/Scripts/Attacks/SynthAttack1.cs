using UnityEngine;

public class SynthAttack1 : MonoBehaviour
{
    public SequencerHandler1 handler;

    Rigidbody rb;

    Vector3 target;
    public float speed;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player1").transform.position;
        transform.LookAt(new Vector3(target.x, 0, target.z));
        transform.Rotate(new Vector3(-90, 0, -90));

        rb = GetComponent<Rigidbody>();

        Vector3 directionForce = new Vector3(target.x, 0, target.z) - new Vector3(transform.position.x, 0, transform.position.z);
        rb.AddForce(Vector3.Normalize(directionForce) * speed, ForceMode.Impulse);
    }


    void Update()
    {
        
    }
}