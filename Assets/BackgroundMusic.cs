using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic _instance;
    public static BackgroundMusic Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GameManager instance is null!");

            return _instance;
        }
    }
    public bool isPlaying;
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(transform.gameObject);
    }
}
