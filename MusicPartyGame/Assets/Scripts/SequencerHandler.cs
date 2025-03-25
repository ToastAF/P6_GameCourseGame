using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SequencerHandler : MonoBehaviour
{
    [SerializeField]
    public List<Beat> beats = new List<Beat>();

    public TextMeshProUGUI beatText;

    public int howManyBeats;
    int beatCounter;
    bool hasCountedUp;

    void Start()
    {
        hasCountedUp = false;

        for(int i = 0; i < howManyBeats; i++)
        {
            beats.Add(new Beat(i));
        }
    }

    void Update()
    {
        beatText.text = "Current beat: " + (beats[beatCounter].number + 1); //We show the beat number + 1 because of indexing starting at 0

        if(hasCountedUp == false)
        {
            StartCoroutine(CountUp(1));
        }
    }

    IEnumerator CountUp(float time)
    {
        hasCountedUp = true;
        beatCounter++;
        if(beatCounter > howManyBeats - 1) { beatCounter = 0; } //This ensures a looping beat
        yield return new WaitForSeconds(time);
        hasCountedUp = false;
    }
}
