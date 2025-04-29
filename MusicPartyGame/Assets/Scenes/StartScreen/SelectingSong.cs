using System;
using UnityEngine;

public class SelectingSong : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] songs;

    public void StartMusic()
    {
        int songIndex = GlobalMusicPicker.selectedSongIndex;
        if (songIndex >= 0 && songIndex < songs.Length)
        {
            audioSource.clip = songs[songIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No valid song selected!");
        }
    }
    
}

