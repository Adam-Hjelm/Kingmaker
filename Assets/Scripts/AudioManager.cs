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


    //[SerializeField, Range(0, 1)] float maxVolume = 1f;
    //[SerializeField, Range(0, 1)] float volumeFive = 0.5f;
    //[SerializeField, Range(0, 1)] float volumeThree = 0.3f;
    //[SerializeField, Range(0, 1)] float volumeTwo = 0.2f;
    //[SerializeField, Range(0, 1)] float volumeOne = 0.1f;


    [Header("Volumes")]
    [SerializeField, Range(0, 1)] float fireballVolume = 0.75f;
    [SerializeField, Range(0, 1)] float dashVolume = 0.75f;
    [SerializeField, Range(0, 1)] float slapVolume = 0.75f;
    [SerializeField, Range(0, 1)] float buttonVolume = 1;
    [SerializeField, Range(0, 1)] float redVolume = 0.75f;
    [SerializeField, Range(0, 1)] float blueVolume = 0.75f;
    [SerializeField, Range(0, 1)] float greenVolume = 0.75f;
    [SerializeField, Range(0, 1)] float pinkVolume = 0.75f;
    [SerializeField, Range(0, 1)] float winVolume = 1;
    [SerializeField, Range(0, 1)] float allWinVolume = 0.75f;


    [Header("Audio Clips")]
    public AudioClip fireballSound;
    public AudioClip dashSound;
    public AudioClip slapSound;
    public AudioClip buttonHoverSound1;
    public AudioClip winSound1;
    public AudioClip allWinSound1;
    public AudioClip redSound;
    public AudioClip blueSound;
    public AudioClip greenSound;
    public AudioClip pinkSound;


    [Header("Other")]
    [SerializeField] AudioSource SoundEffectsSource;
    [SerializeField] AudioSource MusicSource;

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

    #endregion


    public void PlayFireballSound()
    {
        PlaySoundEffect(fireballSound, fireballVolume);
    }

    public void PlayDashSound()
    {
        PlaySoundEffect(dashSound, dashVolume);
    }

    public void PlaySlapSound()
    {
        PlaySoundEffect(slapSound, slapVolume);
    }

    public void PlayButtonHoverSound()
    {
        PlaySoundEffect(buttonHoverSound1, buttonVolume);
    }

    public void PlayWinSound()
    {
        PlaySoundEffect(winSound1, winVolume);
    }

    public void PlayAllWin()
    {
        PlaySoundEffect(allWinSound1, allWinVolume);
    }


    #region Player Sounds

    public void PlayRedSound()
    {
        if (pressed == false)
        {
            PlaySoundEffect(redSound, redVolume);
            StartCoroutine(AntiSpam(redSound.length));
        }
    }

    public void PlayBlueSound()
    {
        if (pressed == false)
        {
            PlaySoundEffect(blueSound, blueVolume);
            StartCoroutine(AntiSpam(blueSound.length));
        }
    }

    public void PlayGreenSound()
    {
        if (pressed == false)
        {
            PlaySoundEffect(greenSound, greenVolume);
            StartCoroutine(AntiSpam(greenSound.length));
        }
    }

    public void PlayPinkSound()
    {
        if (pressed == false)
        {
            PlaySoundEffect(pinkSound, pinkVolume);
            StartCoroutine(AntiSpam(pinkSound.length));
        }
    }

    private IEnumerator AntiSpam(float duration)
    {
        pressed = true;
        yield return new WaitForSeconds(duration);
        pressed = false;
    }

    #endregion
}
