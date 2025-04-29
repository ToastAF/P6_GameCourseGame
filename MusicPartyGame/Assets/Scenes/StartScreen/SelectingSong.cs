using System;
using UnityEngine;

public class SelectingSong : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] songs;

    public void StartMusic()
    {
        audioSource.volume = 0.625f;
        int songIndex = GlobalMusicPicker.selectedSongIndex;
        if (songIndex >= 0 && songIndex < songs.Length)
        {
            audioSource.clip = songs[songIndex];
            audioSource.Play();
            if (songIndex == 1)
            {
                audioSource.volume = 0.4f;
            }
        }
        else
        {
            Debug.LogWarning("No valid song selected!");
        }
    }
    
}

