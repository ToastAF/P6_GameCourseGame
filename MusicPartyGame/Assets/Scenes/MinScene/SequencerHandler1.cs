using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SequencerHandler1 : MonoBehaviour
{
    [SerializeField]
    public List<Beat1> beats = new List<Beat1>();

    public List<RawImage> Top = new List<RawImage>();
    
    public List<RawImage> Mid = new List<RawImage>();
    
    public List<RawImage> Bot = new List<RawImage>();
    public TextMeshProUGUI beatText;

    public int howManyBeats;
    int beatCounter;
    bool hasCountedUp;
    public float time = 0.5f;

    void Start()
    {
        hasCountedUp = false;

        for(int i = 0; i < howManyBeats; i++)
        {
            beats.Add(new Beat1(i));
        }
    }

    void Update()
    {
        beatText.text = "Current beat: " + (beats[beatCounter].number + 1) + " (BPM: " + (60/time) + ")"; //We show the beat number + 1 because of indexing starting at 0

        if(hasCountedUp == false)
        {
            StartCoroutine(CountUp1(time));
        }
    }

    IEnumerator CountUp1(float time)
    {
        hasCountedUp = true;
        beatCounter++;
        if(beatCounter > howManyBeats - 1) { beatCounter = 0; } //This ensures a looping beat
        yield return new WaitForSeconds(time);
        hasCountedUp = false;
    }
}
