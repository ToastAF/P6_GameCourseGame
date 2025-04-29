using NUnit.Framework;
using UnityEngine;

public class BeatChecker : MonoBehaviour
{
    public SequencerHandler1 sequenceHandler;
    GameObject player;
    bool attack1 = false;
    bool attack2 = false;
    bool attack3 = false;
    bool running = false;
    bool knockback = false;
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {        
        bool isTop = sequenceHandler.Top[sequenceHandler.beats[sequenceHandler.beatCounter].number].texture == sequenceHandler.topPicture;
        animator.SetBool("topPicture", isTop);
        bool isMid = sequenceHandler.Mid[sequenceHandler.beats[sequenceHandler.beatCounter].number].texture == sequenceHandler.midPicture;
        animator.SetBool("midPicture", isMid);
        bool isBot = sequenceHandler.Bot[sequenceHandler.beats[sequenceHandler.beatCounter].number].texture == sequenceHandler.botPicture;
        animator.SetBool("botPicture", isBot);
    }

    void CheckNextBeat() 
    {

    }
}
