using System.Collections;
using UnityEngine;

public class PercussionAttack2 : MonoBehaviour
{
    public SequencerHandler1 handler;

    float expireTime;

    void Start()
    {
        expireTime = handler.time;
        StartCoroutine(DestroySelf(expireTime));
    }

    IEnumerator DestroySelf(float time)
    {
        yield return new WaitForSeconds(time);
        
        Destroy(gameObject);
    }
}
