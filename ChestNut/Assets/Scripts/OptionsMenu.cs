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
        fps_25 = 25,
        fps_30 = 31,
        fps_50 = 50,
        fps_60 = 59, 
        fps_120 = 119
    };
    
    // Resolution[] resolutions;
    // public TMP_Dropdown resolutionsDropdown;

    void Start() {
        graphicsDropdown.value = QualitySettings.GetQualityLevel();
        float volumeTmp;
        audioMixer.GetFloat("bgmVolume", out volumeTmp);
        volumeSlider.value = volumeTmp;
        if (GlobalData.FirstTimeEnterMenu) {
            SetFrameRate(frameRateDropdown.value);
        }
        else {
            switch(Application.targetFrameRate) {
                case (int) frameRates.fps_25:
                    frameRateDropdown.value = 0;
                    break;
                case (int) frameRates.fps_30:
                    frameRateDropdown.value = 1;
                    break;
                case (int) frameRates.fps_50:
                    frameRateDropdown.value = 2;
                    break;
                case (int) frameRates.fps_60:
                    frameRateDropdown.value = 3;
                    break;
                case (int) frameRates.fps_120:
                    frameRateDropdown.value = 4;
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
                Application.targetFrameRate = (int) frameRates.fps_25;
                Time.fixedDeltaTime = 1f / 25;
                break;
            case 1:
                Application.targetFrameRate = (int) frameRates.fps_30;
                Time.fixedDeltaTime = 1f / 30;
                break;
            case 2:
                Application.targetFrameRate = (int) frameRates.fps_50;
                Time.fixedDeltaTime = 0.02f;
                break;
            case 3:
                Application.targetFrameRate = (int) frameRates.fps_60;
                Time.fixedDeltaTime = 0.02f;
                break;
            case 4:
                Application.targetFrameRate = (int) frameRates.fps_120;
                Time.fixedDeltaTime = 0.02f;
                break;
        }
    }
}
