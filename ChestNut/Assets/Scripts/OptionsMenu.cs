using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown graphicsDropdown;
    public Slider volumeSlider;
    
    // Resolution[] resolutions;
    // public TMP_Dropdown resolutionsDropdown;

    void Start() {
        graphicsDropdown.value = QualitySettings.GetQualityLevel();
        float volumeTmp;
        audioMixer.GetFloat("bgmVolume", out volumeTmp);
        volumeSlider.value = volumeTmp;

        // int currentResolutionIndex = 0;
        // resolutions = Screen.resolutions;
        // resolutionsDropdown.ClearOptions();
        // List<string> options = new List<string>();
        // for (int i = 0; i < resolutions.Length; i++) {
        //     string option = resolutions[i].width + " x " + resolutions[i].height;
        //     options.Add(option);
        //     if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {
        //         currentResolutionIndex = i;
        //     }
        // }
        // resolutionsDropdown.AddOptions(options);
        // resolutionsDropdown.value = currentResolutionIndex;
        // resolutionsDropdown.RefreshShownValue();
    }

    // public void SetResolution(int resolutionIndex) {
    //     Resolution resolution = resolutions[resolutionIndex];
    //     Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    // }

    public void SetVolume(float volume) {
        audioMixer.SetFloat("bgmVolume", volume);
    }

    public void SetQuality(int quality) {
        QualitySettings.SetQualityLevel(quality);
    }
}
