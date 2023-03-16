using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BGMusicManager : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public Slider bgVolumeSlider;
    public AudioSource bgSource;

    void Update()
    {
        AudioListener.volume = masterVolumeSlider.value;

        bgSource.volume = bgVolumeSlider.value;
    }
}
