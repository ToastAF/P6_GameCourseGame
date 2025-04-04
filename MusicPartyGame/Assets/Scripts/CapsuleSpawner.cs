using System.Collections.Generic;
using UnityEngine;

public class CapsuleSpawner : MonoBehaviour
{
    public GameObject capsulePrefab;
    public int numberOfCapsules = 100;
    public List<GameObject> planes; // List of planes

    void Start()
    {
        for (int i = 0; i < numberOfCapsules; i++)
        {
            // Randomly select a plane
            GameObject selectedPlane = planes[Random.Range(0, planes.Count)];
            Vector3 planeSize = selectedPlane.GetComponent<Renderer>().bounds.size;
            Vector3 planePosition = selectedPlane.transform.position;

            Vector3 position = new Vector3(
                Random.Range(planePosition.x - planeSize.x / 2, planePosition.x + planeSize.x / 2),
                planePosition.y,
                Random.Range(planePosition.z - planeSize.z / 2, planePosition.z + planeSize.z / 2)
            );
            GameObject capsule = Instantiate(capsulePrefab, position, Quaternion.identity);
            capsule.AddComponent<CapsuleJump>();
        }
    }
}