using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class OptionsUI : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    [SerializeField] private Button resetButton;
    [SerializeField] public Slider volumeSlider;

    private Resolution[] resolutions;

    private void Awake()
    {
        // Set the initial value of the volume Slider
        float currentVolume;
        audioMixer.GetFloat("volume", out currentVolume);
        volumeSlider.value = currentVolume;
    }

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        // iteration to determine the current resolution
        for(int i=0; i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resetButton.onClick.AddListener(() =>
        {
            CollectibleManager.instance.ResetProfile();
            LevelManager.instance.ResetProfile();
            SaveManager.instance.ResetData();
        });
    }

    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void setVolume(float volume)
    {
        // set volume parameter according to float volume
        audioMixer.SetFloat("volume", volume);
    }

    public void setFullcreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

}
