using UnityEngine;

public class soundAttack : MonoBehaviour
{
    public void PlaySound(AudioClip clip, Vector3 position, float volume = 1f)
    {
        if (clip == null)
        {
            Debug.LogWarning("SoundPlayer: No clip provided!");
            return;
        }
        
        GameObject tempGO = new GameObject("TempAudio"); // create the temp object
        tempGO.transform.position = position; // set position
        AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add AudioSource
        aSource.clip = clip;
        aSource.volume = volume;
        aSource.spatialBlend = 0f; // 2D sound (if you want 3D sound, set to 1f)
        aSource.Play();
        Destroy(tempGO, clip.length); // destroy after playing
    }
}
