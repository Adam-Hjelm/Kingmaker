using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ConfigScript : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;


    void Start()
    {
        LoadAudioMixerSettings();
    }


    public void LoadAudioMixerSettings()
    {
        SetVolume(PlayerPrefNames.MasterVolume, PlayerPrefs.GetFloat(PlayerPrefNames.MasterVolume, 0.75f));
        SetVolume(PlayerPrefNames.EffectsVolume, PlayerPrefs.GetFloat(PlayerPrefNames.EffectsVolume, 0.75f));
        SetVolume(PlayerPrefNames.MusicVolume, PlayerPrefs.GetFloat(PlayerPrefNames.MusicVolume, 0.75f));
    }

    private void SetVolume(string name, float sliderValue)
    {
        if (sliderValue > 1)
            sliderValue = 1;
        if (sliderValue <= 0)
            sliderValue = 0.0001f;

        float volume = Mathf.Log10(sliderValue) * 20; // THIS IS IMPORTANT! DO NOT TOUCH!
        audioMixer.SetFloat(name, volume);
        PlayerPrefs.SetFloat(name, sliderValue);
    }
}
