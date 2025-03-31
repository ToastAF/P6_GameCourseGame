using UnityEngine;

public class PickUpNotifier : MonoBehaviour
{
    public EnvironmentPickUp spawner;

    // Call this when the pickup is actually collected
    public void OnCollected()
    {
        spawner.PickupCollected(gameObject);
    }
}