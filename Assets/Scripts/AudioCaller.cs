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
        AudioManager.Instance.PlayRedSound();
    }
    public void PlayBlueSound()
    {
        AudioManager.Instance.PlayBlueSound();
    }
    public void PlayGreenSound()
    {
        AudioManager.Instance.PlayGreenSound();
    }
    public void PlayPinkSound()
    {
        AudioManager.Instance.PlayPinkSound();
    }
    public void PlayWinSound()
    {
        AudioManager.Instance.PlayWinSound();
    }
    public void PlayAllWin()
    {
        AudioManager.Instance.PlayAllWin();
    }
}
