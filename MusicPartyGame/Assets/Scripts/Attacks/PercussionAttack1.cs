using System.Collections;
using UnityEngine;

public class PercussionAttack1 : MonoBehaviour
{
    public SequencerHandler1 handler;

    float changeTime;

    private void Start()
    {
        changeTime = handler.time;
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