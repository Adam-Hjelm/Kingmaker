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
    [SerializeField] PlayerInput playerInput;

    
    public void Initialize(InputDevice device)
    {
        this.device = device;
        //var test = new PlayerInput();
        //test.device
        Debug.Log(device.valueType.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (device != null)
        {
            //if (device.valueType)
        }
    }

    void OnReady()
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
    }
}
