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

    public TMP_Dropdown frameRateDropdown;

    private enum frameRates {
        low = 31,
        medium = 59, 
        high = 119
    };
    
    // Resolution[] resolutions;
    // public TMP_Dropdown resolutionsDropdown;

    void Start() {
        graphicsDropdown.value = QualitySettings.GetQualityLevel();
        float volumeTmp;
        audioMixer.GetFloat("bgmVolume", out volumeTmp);
        volumeSlider.value = volumeTmp;
        if (GlobalData.FirstTimeEnterMenu) {
            frameRateDropdown.value = 1;
            SetFrameRate(frameRateDropdown.value);
        }
        else {
            switch(Application.targetFrameRate) {
                case (int) frameRates.low:
                    frameRateDropdown.value = 0;
                    break;
                case (int) frameRates.medium:
                    frameRateDropdown.value = 1;
                    break;
                case (int) frameRates.high:
                    frameRateDropdown.value = 2;
                    break;
            }
        }

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

    public void SetFrameRate(int frameRateIndex) {
        switch (frameRateIndex) {
            case 0:
                Application.targetFrameRate = (int) frameRates.low;
                break;
            case 1:
                Application.targetFrameRate = (int) frameRates.medium;
                break;
            case 2:
                Application.targetFrameRate = (int) frameRates.high;
                break;
        }
    }
}
