using UnityEngine;

public class PickupVisualFX : MonoBehaviour
{
    public float floatAmplitude = 0.25f;
    public float floatFrequency = 1f;
    public float rotationSpeed = 30f;
    // how high above ground you want the "center" of bobbing to be

    private Vector3 startPos;

    private void Start()
    {
        // store the initial position, but force Y to be your desired baseline
        startPos = transform.position;
    }

    private void Update()
    {
        // bob around that base Y
        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        // rotate
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}