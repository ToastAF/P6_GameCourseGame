using UnityEngine;

public class ChildPulsateAndRotate : MonoBehaviour
{
    [Header("Pulsation Settings")]
    public float scaleAmount = 0.05f;       // How much to scale up/down (e.g. 0.05 for 5%)
    public float rotationAmount = 5f;       // Degrees to rotate on X and Z axes
    public float bpm = 60f;                 // Beats per minute, controls pulsation speed

    private Transform[] children;
    private Vector3[] initialScales;
    private Quaternion[] initialRotations;
    private float beatTime;

    void Start()
    {

        // Get child transforms
        int childCount = transform.childCount;
        children = new Transform[childCount];
        initialScales = new Vector3[childCount];
        initialRotations = new Quaternion[childCount];

        for (int i = 0; i < childCount; i++)
        {
            children[i] = transform.GetChild(i);
            initialScales[i] = children[i].localScale;
            initialRotations[i] = children[i].localRotation;
        }
    }

    void Update()
    {
        // Calculate time between beats
        beatTime = 60f / bpm;

        float time = Time.time;
        float beat = Mathf.Sin((time / beatTime) * Mathf.PI * 2f); // Oscillates between -1 and 1
        float scaleFactor = 1f + beat * scaleAmount;
        float rotationOffset = beat * rotationAmount;

        for (int i = 0; i < children.Length; i++)
        {
            // Apply pulsating scale
            children[i].localScale = initialScales[i] * scaleFactor;

            // Apply subtle rotation on X and Z axes
            Quaternion rotX = Quaternion.Euler(rotationOffset, 0f, 0f);
            Quaternion rotZ = Quaternion.Euler(0f, 0f, rotationOffset);
            children[i].localRotation = initialRotations[i] * rotX * rotZ;
        }
    }
}
