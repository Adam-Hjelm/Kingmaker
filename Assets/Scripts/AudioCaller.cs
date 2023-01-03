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
    public void PlayRedSound()
    {
        AudioManager.Instance.RedSound();
    }
    public void PlayBlueSound()
    {
        AudioManager.Instance.GreenSound();
    }
    public void PlayGreenSound()
    {
        AudioManager.Instance.GreenSound();
    }
    public void PlayPinkSound()
    {
        AudioManager.Instance.PinkSound();
    }

}
