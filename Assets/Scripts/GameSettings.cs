using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameSettings : MonoBehaviour
{
    public AudioMixer audioMix;

    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider effectsVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;

    private string masterVolumeName = "MasterVolume";
    private string effectsVolumeName = "EffectsVolume";
    private string musicVolumeName = "MusicVolume";


    void Start()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat(masterVolumeName, 0.75f);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat(effectsVolumeName, 0.75f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat(musicVolumeName, 0.75f);
    }

    #region Volume

    public void SetMasterVolume(float sliderValue)
    {
        SetVolume(masterVolumeName, sliderValue);
    }

    public void SetEffectsVolume(float sliderValue)
    {
        SetVolume(effectsVolumeName, sliderValue);
    }

    public void SetMusicVolume(float sliderValue)
    {
        SetVolume(musicVolumeName, sliderValue);
    }

    private void SetVolume(string name, float sliderValue)
    {
        if (sliderValue > 1)
            sliderValue = 1;
        if (sliderValue <= 0)
            sliderValue = 0.0001f;

        float volume = Mathf.Log10(sliderValue) * 20; // THIS IS IMPORTANT! DO NOT TOUCH!
        audioMix.SetFloat(name, volume);
        PlayerPrefs.SetFloat(name, sliderValue);
    }

    #endregion

    public void SetFullscreen(bool isFullscreen)
    {
        Debug.Log(isFullscreen);
        Screen.fullScreen = isFullscreen;
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
