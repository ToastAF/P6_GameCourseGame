using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PercussionAttack2Spawner : MonoBehaviour
{
    public SequencerHandler1 handler;
    public GameObject player;
    Player1 playerScript;
    public GameObject attack2;

    public Vector3 startPos;
    Vector3 target;

    public float step;

    void Start()
    {
        startPos = transform.position;
        target = GameObject.FindGameObjectWithTag("Player2").transform.position; // Vi finder den anden player
        playerScript = player.GetComponent<Player1>();

        transform.position = Vector3.MoveTowards(transform.position, playerScript.attackDirection.transform.position, step);

        GameObject temp = Instantiate(attack2, transform.position, Quaternion.identity);
        temp.GetComponent<PercussionAttack2>().handler = handler;
    }
}
