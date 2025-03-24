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
        beatText.text = "Current beat: " + beats[beatCounter].number;

        if(hasCountedUp == false)
        {
            StartCoroutine(CountUp(1));
        }
    }

    IEnumerator CountUp(float time)
    {
        hasCountedUp = true;
        beatCounter++;
        yield return new WaitForSeconds(time);
        hasCountedUp = false;
    }
}
