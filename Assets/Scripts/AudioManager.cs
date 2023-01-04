using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GameManager instance is null!");

            return _instance;
        }
    }


    [SerializeField, Range(0, 1)] float maxVolume = 1f;
    [SerializeField, Range(0, 1)] float volumeFive = 0.5f;
    [SerializeField, Range(0, 1)] float volumeThree = 0.3f;
    [SerializeField, Range(0, 1)] float volumeTwo = 0.2f;
    [SerializeField, Range(0, 1)] float volumeOne = 0.1f;

    public AudioClip fireballSound1;
    public AudioClip winSound1;
    public AudioClip buttonHoverSound1;
    public AudioClip allWinSound1;
    public AudioClip redSound;
    public AudioClip blueSound;
    public AudioClip greenSound;
    public AudioClip pinkSound;

    [SerializeField] AudioSource SoundEffectsSource;
    [SerializeField] AudioSource MusicSource;
    //[SerializeField] AudioMixer audioMixer;

    private bool pressed;


    void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(gameObject);
    }


    #region Play functions

    private void PlaySoundEffect(AudioClip clip, float clipVolume)
    {
        SoundEffectsSource.PlayOneShot(clip, clipVolume);
    }

    private void PlayMusic(AudioClip clip, float clipVolume, bool loop)
    {
        MusicSource.loop = loop;
        MusicSource.PlayOneShot(clip, clipVolume);
    }

    //private void SetAudioMixerGroup(string audioMixerGroupName)
    //{
    //    var mixerGroup = audioMixer.FindMatchingGroups(audioMixerGroupName).FirstOrDefault();
    //    if (mixerGroup == null)
    //        Debug.LogError($"No AudioMixerGroup of name '{audioMixerGroupName}' exists!");
    //    SoundEffectsSource.outputAudioMixerGroup = mixerGroup;
    //}

    #endregion


    public void PlayWinSound()
    {
        PlaySoundEffect(winSound1, maxVolume);
    }

    public void PlayButtonHoverSound()
    {
        PlaySoundEffect(buttonHoverSound1, maxVolume);
    }

    public void PlayRedSound()
    {
        if (pressed == false)
        {
            PlaySoundEffect(redSound, volumeOne);
            StartCoroutine(AntiSpam());
        }
    }

    public void PlayBlueSound()
    {
        if (pressed == false)
        {
            PlaySoundEffect(blueSound, volumeOne);
            StartCoroutine(AntiSpam());
        }
    }

    public void PlayGreenSound()
    {
        if (pressed == false)
        {
            PlaySoundEffect(greenSound, volumeOne);
            StartCoroutine(AntiSpam());
        }
    }

    public void PlayPinkSound()
    {
        if (pressed == false)
        {
            PlaySoundEffect(pinkSound, volumeOne);
            StartCoroutine(AntiSpam());
        }
    }

    public void PlayAllWin()
    {
        PlaySoundEffect(allWinSound1, volumeTwo);
    }

    private IEnumerator AntiSpam()
    {
        pressed = true;
        yield return new WaitForSeconds(2f);
        pressed = false;
    }
}
