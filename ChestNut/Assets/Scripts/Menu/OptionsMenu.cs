using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public TMP_Dropdown graphicsDropdown;
    public TMP_Dropdown frameRateDropdown;
    public TMP_Dropdown controlModeDropdown;

    private enum frameRates {
        fps_25 = 26,
        fps_30 = 31,
        fps_50 = 51,
        fps_60 = 61, 
        fps_120 = 121
    };
    
    // Resolution[] resolutions;
    // public TMP_Dropdown resolutionsDropdown;

    void Start() {
        if (GlobalData.FirstTimeEnterMenu) {
            SetVolume(volumeSlider.value);
            SetQuality(graphicsDropdown.value);
            SetFrameRate(frameRateDropdown.value);
            SetControlMode(controlModeDropdown.value);
        }
        else {
            float volumeTmp;
            audioMixer.GetFloat("bgmVolume", out volumeTmp);
            volumeSlider.value = volumeTmp;
            graphicsDropdown.value = QualitySettings.GetQualityLevel();
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
            controlModeDropdown.value = GlobalData.controlMode;
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
        GlobalData.needSwitchSnowRenderMode = quality < 5;
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

    public void SetControlMode(int controlMode) {
        GlobalData.controlMode = controlMode;
    }
}
