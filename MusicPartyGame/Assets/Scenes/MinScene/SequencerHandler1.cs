using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;

public class SequencerHandler1 : MonoBehaviour
{
    public GameObject attack1, attack2, attack3, player;
    public bool isPlayer1; //Den her styrer hvilke attacks der bliver affyret

    public AudioSource att1, att2, att3;

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
    public int beatCounter;
    public bool hasCountedUp;

    public float BPM_input;
    public float time;
    
    public int previousBeatIndex = -1;

    void Start()
    {
        time = 60 / BPM_input;

        att1.volume = 0;
        
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
        beatText.text = "Current beat: " + (beats[beatCounter].number + 1) + " (BPM: " + BPM_input + ")"; //We show the beat number + 1 because of indexing starting at 0

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

        if (isTop)
        {
            att1.volume = 100;
            
        }
        else
        {
            att1.volume = 0;
        }

// Prioritized checks (most complex combos first)  ~~~~~~~~~~ Det her skal kun bruges hvis vi har tid til det
       /* if (isTop && isMid && isBot)
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
        }*/
        if (isTop)
        {
            Debug.Log("Attack 1");
            if (isPlayer1 == true) // Player1's Attack1
            {
                GameObject temp = Instantiate(attack1, new Vector3(player.transform.position.x, 0, player.transform.position.z), Quaternion.Euler(-90, 0, 0));
                temp.GetComponent<PercussionAttack1>().handler = GetComponent<SequencerHandler1>();
            }
            else if(isPlayer1 == false) // Player2's Attack1
            {
                GameObject temp = Instantiate(attack1, new Vector3(player.transform.position.x, 0, player.transform.position.z), Quaternion.Euler(-90, 0, 0));
                temp.GetComponent<SynthAttack1>().handler = GetComponent<SequencerHandler1>();
                temp.GetComponent<SynthAttack1>().player = player;
            }

        }
        else if (isMid)
        {
            Debug.Log("Attack 2");
            if (isPlayer1 == true) // Player1's Attack2 ~ Percussion
            {
                GameObject temp = Instantiate(attack2, player.transform.position, Quaternion.identity);
                temp.GetComponent<PercussionAttack2Spawner>().handler = GetComponent<SequencerHandler1>(); // Attack2 her bruger en spawner
                temp.GetComponent<PercussionAttack2Spawner>().player = player;
                temp.transform.rotation = player.transform.rotation;
            }
            else if (isPlayer1 == false) // Player2's Attack2 ~ Synth
            {
                GameObject temp = Instantiate(attack2, player.transform.position, Quaternion.identity);
                temp.GetComponent<SynthAttack2>().handler = GetComponent<SequencerHandler1>();
                temp.GetComponent<SynthAttack2>().player = player;

            }
        }
        else if (isBot)
        {
            Debug.Log("Attack 3");
            if (isPlayer1 == true)
            {
                GameObject temp = Instantiate(attack3, player.transform.position, Quaternion.identity);
                temp.GetComponent<PercussionAttack3>().player = player;
            }
            else if(isPlayer1 == false) // Player2's Attack3 ~ Synth
            {
                GameObject temp = Instantiate(attack3, player.transform.position, Quaternion.identity);
                temp.GetComponent<SynthAttack3>().player = player;
            }
        }

    }
}
