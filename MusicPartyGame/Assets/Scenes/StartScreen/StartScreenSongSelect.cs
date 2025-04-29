using UnityEngine;
using TMPro; // For TMP_Dropdown

public class StartScreenSongSelect : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] songs;
    public TMP_Dropdown dropdown;

    void Start()
    {
        dropdown.onValueChanged.AddListener(OnSongSelected);

        // Optional: Play the first song immediately when the scene loads
        OnSongSelected(dropdown.value);
    }

    void OnDestroy()
    {
        dropdown.onValueChanged.RemoveListener(OnSongSelected);
    }

    void OnSongSelected(int index)
    {
        audioSource.volume = 0.625f;
        if (index >= 0 && index < songs.Length)
        {
            audioSource.clip = songs[index];
            audioSource.Play();

            // Save the selected song for the next scene
            GlobalMusicPicker.selectedSongIndex = index;
            if (index == 1)
            {
                audioSource.volume = 0.4f;
            }
        }
        else
        {
            Debug.LogWarning("Invalid song index selected!");
        }
    }
}