using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SettingsCon : MonoBehaviour
{
    public AudioMixer audioMix;
    public void SetMasterVolume(float volume)
    {
        audioMix.SetFloat("volume", volume);
        Debug.Log(volume);
    }
    public void SetEffectVolume(float volume)
    {
        audioMix.SetFloat("volume", volume);
        Debug.Log(volume);
    }
    public void SetBackgroundVolume(float volume)
    {
        audioMix.SetFloat("volume", volume);
        Debug.Log(volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
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
