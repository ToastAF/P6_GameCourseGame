using UnityEngine;

public class TestAttack : MonoBehaviour
{
    Vector3 target = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.LookAt(target); // Den her skal ændre til at kigge mod skyderetning
        Vector3 directionForce = new Vector3(target.x, 0, target.z) - new Vector3(transform.position.x, 0, transform.position.z);
        GetComponent<Rigidbody>().AddForce(Vector3.Normalize(directionForce), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
