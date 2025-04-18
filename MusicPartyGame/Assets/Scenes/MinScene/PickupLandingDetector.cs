using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickupLandingDetector : MonoBehaviour
{
    private bool landed = false;

    void OnCollisionEnter(Collision collision)
    {
        // If we've already landed once, do nothing more
        if (landed) return;

        // Check if this collision is with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            landed = true;
            Debug.Log("I landed! :)");
            
            Vector3 pos = transform.position;
            pos.y = 0.6f;
            transform.position = pos;

            // Stop physics so it stays in place
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;

            // Convert collider to trigger so players can walk through
            Collider col = GetComponent<Collider>();
            col.isTrigger = true;

            // Re-enable the floating effect
            PickupVisualFX fx = GetComponent<PickupVisualFX>();
            if (fx != null)
            {
                fx.enabled = true;

                // If you want the pickup to stop moving,
                // you could set the rigidbody to kinematic or disable gravity:
                // Rigidbody rb = GetComponent<Rigidbody>();
                // rb.isKinematic = true;
                // or rb.useGravity = false;
            }
        }
    }
}