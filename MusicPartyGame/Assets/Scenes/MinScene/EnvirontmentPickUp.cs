using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class EnvironmentPickUp : MonoBehaviour
{
    public Transform mapArea;        // Where pickups should ultimately land
    public Transform pickupSpawner;  // The transform from where pickups are "shot"
    
    public GameObject pickupTopPrefab;
    public GameObject pickupMidPrefab;
    public GameObject pickupBotPrefab;

    public int maxPickups = 6;
    private List<GameObject> currentPickups = new List<GameObject>();

    private bool isSpawning = false;

    void Start()
    {
        // Start by spawning initial batch
        for (int i = 0; i < maxPickups; i++)
        {
            SpawnPickup();
        }

        // Start auto-spawn manager
        StartCoroutine(AutoSpawnManager());
    }

    public void PickupCollected(GameObject pickup)
    {
        if (currentPickups.Contains(pickup))
        {
            currentPickups.Remove(pickup);
            Destroy(pickup);
        }
    }

    IEnumerator AutoSpawnManager()
    {
        while (true)
        {
            if (currentPickups.Count < maxPickups && !isSpawning)
            {
                // Delay the spawn, so not everything spawns at once
                StartCoroutine(SpawnPickupAfterDelay(5f));
            }

            yield return new WaitForSeconds(1f); // Check every second
        }
    }

    IEnumerator SpawnPickupAfterDelay(float delay)
    {
        isSpawning = true;
        yield return new WaitForSeconds(delay);

        if (currentPickups.Count < maxPickups)
        {
            SpawnPickup();
        }

        isSpawning = false;
    }

    void SpawnPickup()
    {
        // [1] Find a random valid landing position, as you do now.
        Vector3 landPos = GetRandomPositionWithinMap();
        if (landPos == Vector3.zero)
            return;

        // [2] Instantiate
        GameObject prefabToSpawn = GetRandomPickupPrefab();
        GameObject newPickup = Instantiate(prefabToSpawn, pickupSpawner.position, Quaternion.identity, mapArea);
        currentPickups.Add(newPickup);
        
        // Ensure a Rigidbody
        
        // Ensure collider is initially trigger = true, to avoid collisions with spawner
        Collider col = newPickup.GetComponent<Collider>();
        if (col == null)
        {
            col = newPickup.AddComponent<SphereCollider>(); // Or BoxCollider, etc.
        }
        col.isTrigger = true;

        // Add the script that will switch 'isTrigger' off after a time
        FlightTriggerToggle toggle = newPickup.AddComponent<FlightTriggerToggle>();
        toggle.disableTriggerDelay = 0.5f; // or whatever delay you need

        // Add or confirm there's a Rigidbody
        Rigidbody rb = newPickup.GetComponent<Rigidbody>();
        if (rb == null) rb = newPickup.AddComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

        // [3] Add or ensure we have a rigidbody, then launch
        LaunchPickup(newPickup, landPos);

        // [4] Disable PickupVisualFX so it won't animate while airborne
        PickupVisualFX fx = newPickup.GetComponent<PickupVisualFX>();
        if (fx != null)
        {
            fx.enabled = false;
        }

        // [5] Add the notifier so you can remove from currentPickups on collect
        PickUpNotifier notifier = newPickup.AddComponent<PickUpNotifier>();
        notifier.spawner = this;

        // [6] Add a script to detect landing:
        newPickup.AddComponent<PickupLandingDetector>();
    }


    void LaunchPickup(GameObject pickup, Vector3 landPos)
    {
        Rigidbody rb = pickup.GetComponent<Rigidbody>();
        if (rb == null)
        {
            // If your prefab doesn't have a Rigidbody, add one here (or add in the prefab editor)
            rb = pickup.AddComponent<Rigidbody>();
            rb.useGravity = true;
        }

        // Calculate a ballistic trajectory so that the pickup lands around 'landPos'
        // ----------------------------------------------------------
        float flightTime = 2.0f;  // Adjust to make the pickup fly longer/shorter
        Vector3 toTarget = landPos - pickupSpawner.position;

        // We'll separate horizontal vs. vertical components
        Vector3 toTargetXZ = toTarget;
        toTargetXZ.y = 0f; // remove vertical difference for horizontal magnitude

        float horizontalDistance = toTargetXZ.magnitude;
        float verticalDistance = toTarget.y;

        float gravity = Mathf.Abs(Physics.gravity.y); // typically 9.81f

        // Horizontal velocity is distance / time
        float horizontalSpeed = horizontalDistance / flightTime;
        Vector3 launchVelocityXZ = toTargetXZ.normalized * horizontalSpeed;

        // For vertical velocity, use: Vy = (Δy + 0.5*g*t²) / t
        float verticalSpeed = (verticalDistance + 0.5f * gravity * (flightTime * flightTime)) / flightTime;

        // Combine horizontal + vertical into one velocity vector
        Vector3 launchVelocity = launchVelocityXZ + (verticalSpeed * Vector3.up);
        rb.linearVelocity = launchVelocity;
        // ----------------------------------------------------------
    }

    GameObject GetRandomPickupPrefab()
    {
        int choice = Random.Range(0, 3);
        if (choice == 0) return pickupTopPrefab;
        if (choice == 1) return pickupMidPrefab;
        return pickupBotPrefab;
    }

    Vector3 GetRandomPositionWithinMap()
    {
        // Same random selection as before
        Vector3 center = mapArea.position;

        float halfWidth = 5f * mapArea.localScale.x;
        float halfLength = 5f * mapArea.localScale.z;

        float x = Random.Range(center.x - halfWidth, center.x + halfWidth);
        float z = Random.Range(center.z - halfLength, center.z + halfLength);

        // The 0.5f offset is to keep the pickup from sinking into the ground
        return new Vector3(x, center.y + 0.5f, z);
    }
}
