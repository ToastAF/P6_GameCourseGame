using System.Collections;
using UnityEngine;

public class PercussionAttack1 : MonoBehaviour
{
    public SequencerHandler1 handler;

    float stage = 1;

    float changeTime;

    private void Start()
    {
        changeTime = handler.time;
        StartCoroutine(ChangeCD(changeTime));
    }

    void Update()
    {
        /*if (stage == 2)
        {
            transform.localScale = new Vector3(200, 200, 200);
        }
        else if (stage == 3)
        {
            transform.localScale = new Vector3(300, 300, 300);
        }
        if(stage > 3)
        {
            Destroy(gameObject);
        }*/
    }

    IEnumerator ChangeCD(float time)
    {
        //Wait before expanding!!!
        yield return new WaitForSeconds(time);

        transform.localScale = new Vector3(200, 200, 200);
        Debug.Log("Stage UP!");
        yield return new WaitForSeconds(time);

        transform.localScale = new Vector3(300, 300, 300);
        Debug.Log("Stage UP!");
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}