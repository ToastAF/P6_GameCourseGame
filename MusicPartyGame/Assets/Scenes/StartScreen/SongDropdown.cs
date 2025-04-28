using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SongDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    public void OnSongSelected()
    {
        GlobalMusicPicker.selectedSongIndex = dropdown.value;
        Debug.Log(GlobalMusicPicker.selectedSongIndex);
    }


}
