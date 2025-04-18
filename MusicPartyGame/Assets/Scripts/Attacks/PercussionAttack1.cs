using System.Collections;
using UnityEngine;

public class PercussionAttack1 : MonoBehaviour
{
    public SequencerHandler1 handler;

    float changeTime;

    public GameObject stage1, stage2, stage3;

    private void Start()
    {
        changeTime = handler.time;
        StartCoroutine(ChangeCD(changeTime));
    }

    IEnumerator ChangeCD(float time)
    {
        GameObject temp1 = Instantiate(stage1, transform.position, Quaternion.identity);
        //Wait before expanding!!!
        yield return new WaitForSeconds(time);

        Destroy(temp1);
        GameObject temp2 = Instantiate(stage2, transform.position, Quaternion.identity);
        //Debug.Log("Stage UP!");
        yield return new WaitForSeconds(time);

        Destroy(temp2);
        GameObject temp3 = Instantiate(stage3, transform.position, Quaternion.identity);
        //Debug.Log("Stage UP!");
        yield return new WaitForSeconds(time);

        Destroy(temp3);
    }
}