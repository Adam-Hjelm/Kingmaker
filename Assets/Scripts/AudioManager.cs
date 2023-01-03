using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public AudioClip fireballSound1;
    public AudioClip winSound1;
    public AudioClip buttonHoverSound1;

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
        source.PlayOneShot(winSound1);
    }

    public void PlayButtonHoverSound()
    {
        source.PlayOneShot(buttonHoverSound1);
    }
}
