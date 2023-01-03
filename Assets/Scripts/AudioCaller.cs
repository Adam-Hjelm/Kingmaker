using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCaller : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayButtonHoverSound()
    {
        AudioManager.Instance.PlayButtonHoverSound();
    }
}
