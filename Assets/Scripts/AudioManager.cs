using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public float maxVolume = 1f;
    public float volumeFive = 0.5f;
    public float volumeThree = 0.3f;
    public float volumeTwo = 0.2f;
    public float volumeOne = 0.1f;

    private bool pressed;

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


    public AudioClip fireballSound1;
    public AudioClip winSound1;
    public AudioClip buttonHoverSound1;
    public AudioClip allWinSound1;
    public AudioClip redSound;
    public AudioClip blueSound;
    public AudioClip greenSound;
    public AudioClip pinkSound;


    [SerializeField] AudioSource source;


    void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;
    }

    public void PlayWinSound()
    {
        source.volume = maxVolume;
        source.PlayOneShot(winSound1);
    }

    public void PlayButtonHoverSound()
    {
        source.volume = volumeThree;
        source.PlayOneShot(buttonHoverSound1);
    }

    public void RedSound()
    {
        if (pressed == false)
        {
            source.volume = volumeOne;
            source.PlayOneShot(redSound);
            StartCoroutine(AntiSpam());
        }
    }
    public void BlueSound()
    {
        if (pressed == false)
        {
            source.volume = volumeOne;
            source.PlayOneShot(blueSound);
            StartCoroutine(AntiSpam());
        }
    }
    public void GreenSound()
    {
        if (pressed == false)
        {
            source.volume = volumeOne;
            source.PlayOneShot(greenSound);
            StartCoroutine(AntiSpam());
        }
    }
    public void PinkSound()
    {
        if (pressed == false)
        {
            source.volume = volumeOne;
            source.PlayOneShot(pinkSound);
            StartCoroutine(AntiSpam());
        }
    }

    public void AllWin()
    {
        source.volume = volumeTwo;
        source.PlayOneShot(allWinSound1);
    }

    private IEnumerator AntiSpam()
    {
        pressed = true;
        yield return new WaitForSeconds(2f);

        pressed = false;
    }
}
