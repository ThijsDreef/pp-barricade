using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    public Dropdown resolutionDropdown;
    public AudioMixer audioMixer;
    private Resolution[] resolutions;
 

    private void Start() {
        ResolutionsCheck();
    }

    /// <summary> Collect Resolution options for dropdown and add it to the dropdown UI </summary>
    public void ResolutionsCheck() {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height) {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    /// <summary> Adjust chosen resolution to the screen </summary>
    public void SetResolution (int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    /// Change mastervolume in audioMixer
    public void SetMasterVolume(float volume) {
        SetVolume("MasterVolume", volume);
    }
    
    public void SetMusicVolume(float volume) {
        SetVolume("MusicVolume", volume);
    }

    public void SetEffectsVolume(float volume) {
        SetVolume("EffectsVolume", volume);
    }

    /// <summary> Change volume in audioMixer </summary>
    private void SetVolume(string name, float volume) {
            audioMixer.SetFloat(name, volume);
        }

    /// <summary> Adjust project settings graphic settings </summary>
    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    /// <summary> Toggle fullscreen on / off </summary>
    public void SetFullscreen (bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }
}