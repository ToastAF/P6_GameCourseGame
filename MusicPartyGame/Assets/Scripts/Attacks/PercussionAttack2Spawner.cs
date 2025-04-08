using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PercussionAttack2Spawner : MonoBehaviour
{
    public SequencerHandler1 handler;
    public GameObject attack2;

    public Vector3 startPos;
    Vector3 target;


    void Start()
    {
        startPos = transform.position;
        target = GameObject.FindGameObjectWithTag("Player1").transform.position; // Vi finder den anden player

        Vector3 directionVector = new Vector3(target.x, 0, target.z) - new Vector3(transform.position.x, 0, transform.position.z); // Finder retningen mod den anden player

        GameObject temp = Instantiate(attack2, startPos + Vector3.Normalize(directionVector), Quaternion.identity);
        temp.GetComponent<PercussionAttack2>().handler = handler;
    }
}
