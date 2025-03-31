using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class EnvironmentPickUp : MonoBehaviour
{
    public Transform mapArea; // Where to spawn pickups
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
        const int maxAttempts = 20;
        const float minDistanceBetweenPickups = 1.5f; // Adjust as needed

        Vector3 spawnPos = Vector3.zero;
        bool validPosition = false;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            spawnPos = GetRandomPositionWithinMap();
            validPosition = true;

            foreach (GameObject pickup in currentPickups)
            {
                if (Vector3.Distance(spawnPos, pickup.transform.position) < minDistanceBetweenPickups)
                {
                    validPosition = false;
                    break;
                }
            }

            if (validPosition)
                break;
        }

        if (!validPosition)
        {
            Debug.LogWarning("Failed to find a valid spawn position after multiple attempts.");
            return;
        }

        GameObject prefabToSpawn = GetRandomPickupPrefab();
        GameObject newPickup = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity, mapArea);
        currentPickups.Add(newPickup);

        PickUpNotifier notifier = newPickup.AddComponent<PickUpNotifier>();
        notifier.spawner = this;
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
        Vector3 center = mapArea.position;

        float halfWidth = 5f * mapArea.localScale.x;
        float halfLength = 5f * mapArea.localScale.z;

        float x = Random.Range(center.x - halfWidth, center.x + halfWidth);
        float z = Random.Range(center.z - halfLength, center.z + halfLength);

        return new Vector3(x, center.y + 0.5f, z);
    }
}
