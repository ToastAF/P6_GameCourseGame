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

    public Texture topPicture;
    public Texture midPicture;
    public Texture botPicture;

    public int howManyBeats;
    int beatCounter;
    bool hasCountedUp;
    public float time = 0.5f;
    
    int previousBeatIndex = -1;

    void Start()
    {
        hasCountedUp = false;

        for(int i = 0; i < howManyBeats; i++)
        {
            beats.Add(new Beat1(i));
        }
        
        foreach (var img in Top) ApplyGlow(img, false);
        foreach (var img in Mid) ApplyGlow(img, false);
        foreach (var img in Bot) ApplyGlow(img, false);
        
    }

    public void pickUpPickedUp(int level)
    {
        if (level == 0)
        {
          Top[beats[beatCounter].number].texture = topPicture;
        }
        if (level == 1)
        {
            Mid[beats[beatCounter].number].texture = midPicture;
        }
        if (level == 2)
        {
            Bot[beats[beatCounter].number].texture = botPicture;
        }
    }
    
    void ApplyGlow(RawImage image, bool enable)
    {
        if (image == null) return;

        Outline outline = image.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = enable;
            if (enable) outline.effectColor = Color.yellow;
        }

        // Check if the image's texture is one of the active pictures
        if (enable && (image.texture == topPicture || image.texture == midPicture || image.texture == botPicture))
        {
            StartCoroutine(AnimateGlow(image));
        }
        else
        {
            StopCoroutine(AnimateGlow(image));
            image.transform.localScale = Vector3.one;
        }
    }
    
    IEnumerator AnimateGlow(RawImage image)
    {
        Vector3 originalScale = Vector3.one;
        Vector3 targetScale = originalScale * 1.1f; // Slight pulse

        float t = 0f;
        float duration = 0.2f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float scale = Mathf.Lerp(1f, 1.1f, t / duration);
            image.transform.localScale = new Vector3(scale, scale, 1f);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f); // Pause slightly

        // Return to normal
        t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float scale = Mathf.Lerp(1.1f, 1f, t / duration);
            image.transform.localScale = new Vector3(scale, scale, 1f);
            yield return null;
        }
    }

    void Update()
    {
        beatText.text = "Current beat: " + (beats[beatCounter].number + 1) + " (BPM: " + (60/time) + ")"; //We show the beat number + 1 because of indexing starting at 0

        if (!IsInvoking(nameof(AdvanceBeat)))
        {
            Invoke(nameof(AdvanceBeat), time);
        }

    }
    
    void AdvanceBeat()
    {
        StartCoroutine(CountUp1(time));
    }

    IEnumerator CountUp1(float time)
    {
        hasCountedUp = true;

        if (previousBeatIndex != -1)
        {
            ApplyGlow(Top[beats[previousBeatIndex].number], false);
            ApplyGlow(Mid[beats[previousBeatIndex].number], false);
            ApplyGlow(Bot[beats[previousBeatIndex].number], false);
        }
        
        beatCounter++;
        if(beatCounter > howManyBeats - 1) { beatCounter = 0; } //This ensures a looping beat
        
        ApplyGlow(Top[beats[beatCounter].number], true);
        ApplyGlow(Mid[beats[beatCounter].number], true);
        ApplyGlow(Bot[beats[beatCounter].number], true);
        
        previousBeatIndex = beatCounter;
        
        yield return new WaitForSeconds(time);
        hasCountedUp = false;
        
        
        bool isTop = Top[beats[beatCounter].number].texture == topPicture;
        bool isMid = Mid[beats[beatCounter].number].texture == midPicture;
        bool isBot = Bot[beats[beatCounter].number].texture == botPicture;

// Prioritized checks (most complex combos first)
        if (isTop && isMid && isBot)
        {
            Debug.Log("ULTIMATE COMBO ATTACK!");
        }
        else if (isTop && isBot)
        {
            Debug.Log("Combo Attack: Top + Bot");
        }
        else if (isTop && isMid)
        {
            Debug.Log("Combo Attack: Top + Mid");
        }
        else if (isMid && isBot)
        {
            Debug.Log("Combo Attack: Mid + Bot");
        }
        else if (isTop)
        {
            Debug.Log("Attack 1");
        }
        else if (isMid)
        {
            Debug.Log("Attack 2");
        }
        else if (isBot)
        {
            Debug.Log("Attack 3");
        }

    }
}
