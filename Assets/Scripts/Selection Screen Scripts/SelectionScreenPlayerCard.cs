using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionScreenPlayerCard : MonoBehaviour
{
    InputDevice device;
    bool isReady = false;

    [SerializeField] GameObject readyBtn;
    [SerializeField] GameObject notReadyBtn;

    
    public void Initialize(InputDevice device)
    {
        this.device = device;
    }

    public void OnReady()
    {
        isReady = !isReady;
        Debug.Log("on ready");
        if (isReady)
        {
            readyBtn.SetActive(true);
            notReadyBtn.SetActive(false);
        }
        else
        {
            readyBtn.SetActive(false);
            notReadyBtn.SetActive(true);
        }

        SelectionScreen.Instance.PlayerChangedReadyState(device, isReady);
    }

    private void OnLeave()
    {
        SelectionScreen.Instance.RemovePlayer(device);
    }
}
