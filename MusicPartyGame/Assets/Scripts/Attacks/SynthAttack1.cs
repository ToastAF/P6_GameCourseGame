using System.Collections;
using UnityEngine;

public class SynthAttack1 : MonoBehaviour
{
    public SequencerHandler1 handler;

    Rigidbody rb;

    Vector3 target;
    public float speed;
    float changeTime;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player1").transform.position; // Vi finder den anden player
        transform.LookAt(new Vector3(target.x, 0, target.z));
        transform.Rotate(new Vector3(-90, 0, -90)); // Roterer game objectet fordi den vender mærkeligt

        rb = GetComponent<Rigidbody>();

        Vector3 directionForce = new Vector3(target.x, 0, target.z) - new Vector3(transform.position.x, 0, transform.position.z); // Finder retningen mod den anden player
        rb.AddForce(Vector3.Normalize(directionForce) * speed, ForceMode.Impulse); // Og skyder den af sted

        changeTime = handler.time; // Her fra og ned er det det samme som PercussionAttack1
        StartCoroutine(ChangeCD(changeTime));
    }

    IEnumerator ChangeCD(float time)
    {
        //Wait before expanding!!!
        yield return new WaitForSeconds(time);

        transform.localScale = transform.localScale * 2;
        //Debug.Log("Stage UP!");
        yield return new WaitForSeconds(time);

        transform.localScale = transform.localScale * 1.5f;
        //Debug.Log("Stage UP!");
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}