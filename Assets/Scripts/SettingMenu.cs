using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    #region My Variable's
    [Header("Setting Setup")]
    public AudioMixer audioMixer;

    public TMP_Dropdown screenResolutionDropdown;

    public TMP_Dropdown graphicQualityDropdown;

    public Toggle fullscreenToggle;

    public Slider volumeSlider;

    private Resolution[] resolutions;

    #endregion My Variable's

    public void Start()
    {
        float loadSaveVolume = PlayerPrefs.GetFloat("MainVolume", 0.7f);
        SetVolume(loadSaveVolume);
        volumeSlider.value = loadSaveVolume;

        SetupFullscreen();
        SetupScreenResolution();
        SetupGraphicQuality();
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("MainVolume", volume);
        audioMixer.SetFloat("MainVolume", volume);
    }

    #region Screen Resolution
    private void SetupScreenResolution()
    {
        // Load all available screen resolition for this app.
        resolutions = Screen.resolutions;

        // Clear out the default items on dropdown
        screenResolutionDropdown.ClearOptions();

        // SesolutionDropdown only accpet list of string
        List<string> resolutionList = new List<string>();

        int currentResolutionIndex = 0;

        // for each resolutions we addded to a list then,
        int resolutionIndex = 0;
        foreach (Resolution resolution in resolutions)
        {
            string _resolution = resolution.width + " x " + resolution.height;

            if (resolution.width.Equals(Screen.currentResolution.width) && resolution.height.Equals(Screen.currentResolution.height))
            {
                Debug.Log("Current screen resolution: " + _resolution);
                currentResolutionIndex = resolutionIndex;
            }

            resolutionList.Add(_resolution);

            resolutionIndex++;
        }

        // load the list of string resolution items to a dropdown list
        screenResolutionDropdown.AddOptions(resolutionList);

        // after load, select the current screenResolution
        screenResolutionDropdown.value = currentResolutionIndex;

        screenResolutionDropdown.RefreshShownValue();

        Debug.Log(("Selected screen resolution : " + resolutionList[currentResolutionIndex])
          + " with index of: " + currentResolutionIndex);
    }

    public void SetScreenResolution(int screenResolutionIndex)
    {
        Resolution resolution = resolutions[screenResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        Debug.Log(("Screen resolution set to: " + Screen.resolutions[screenResolutionIndex])
            + " with the current index of: " + screenResolutionIndex);
    }

    #endregion Screen Resolution

    #region Fullscreen
    public void SetupFullscreen()
    {
        SetFullscreen(Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        fullscreenToggle.isOn = isFullscreen;
        Debug.Log("Fullscreen Mode " + Screen.fullScreen);
    }
    #endregion Fullscreen

    #region Graphics Quality
    private void SetupGraphicQuality()
    {
        // Clear all items on a dropdown 
        graphicQualityDropdown.ClearOptions();

        // Create list of quality to load on dropdown
        List<string> qualityList = new List<string>(QualitySettings.names);

        // add the list of quality to the dropdown
        graphicQualityDropdown.AddOptions(qualityList);

        graphicQualityDropdown.value = QualitySettings.GetQualityLevel();

        Debug.Log("Current Graphic Quality: " + QualitySettings.names[QualitySettings.GetQualityLevel()].ToString());

        graphicQualityDropdown.RefreshShownValue();
    }

    public void SetGraphicQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        Debug.Log(QualitySettings.names[qualityIndex].ToString() + ", with the index of : " + qualityIndex);
    }
    #endregion Graphics Quality

}
