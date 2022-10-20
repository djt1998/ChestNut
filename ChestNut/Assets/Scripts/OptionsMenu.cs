using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject graphics;
    public GameObject volumeSlider;

    void Start() {
        graphics.GetComponent<TMP_Dropdown>().value = QualitySettings.GetQualityLevel();
        float volumeTmp;
        audioMixer.GetFloat("bgmVolume", out volumeTmp);
        volumeSlider.GetComponent<Slider>().value = volumeTmp;
    }

    public void SetVolume(float volume) {
        audioMixer.SetFloat("bgmVolume", volume);
    }

    public void SetQuality(int quality) {
        QualitySettings.SetQualityLevel(quality);
    }
}
