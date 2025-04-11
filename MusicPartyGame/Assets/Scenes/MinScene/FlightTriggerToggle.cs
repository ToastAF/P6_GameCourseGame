using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class FlightTriggerToggle : MonoBehaviour
{
    public float disableTriggerDelay = 0.5f;

    void OnEnable()
    {
        // Start a coroutine that will flip off 'isTrigger' after a delay
        StartCoroutine(DisableTriggerAfterDelay(disableTriggerDelay));
    }

    private IEnumerator DisableTriggerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Turn off the trigger so the pickup can collide with ground
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = false;
        }
    }
}