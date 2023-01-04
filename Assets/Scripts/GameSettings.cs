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
    //public float maxVolume;
    //public float volume;


    #region Volume

    public void SetMasterVolume(float volume)
    {
        audioMix.SetFloat("volume", volume);
        Debug.Log(volume);
    }

    public void SetEffectsVolume(float volume)
    {
        audioMix.SetFloat("volume", volume);
        Debug.Log(volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMix.SetFloat("volume", volume);
        Debug.Log(volume);
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
