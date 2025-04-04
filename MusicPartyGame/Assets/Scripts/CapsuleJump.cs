using UnityEngine;

public class CapsuleJump : MonoBehaviour
{
    public float jumpHeight = 1f;
    public float jumpSpeed = 1f;
    private Vector3 startPosition;
    private float startTime;

    void Start()
    {
        startPosition = transform.position;
        startTime = Random.Range(0f, 2f * Mathf.PI); // Randomize the start time for independent jumping
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * jumpSpeed + startTime) * jumpHeight;
        newY = Mathf.Max(newY, startPosition.y); // Ensure the capsule does not go below its initial height
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}