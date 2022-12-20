using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionScreenPlayerCard : MonoBehaviour
{
    InputDevice device;
    bool isReady = false;

    [SerializeField] GameObject readyBtn;
    [SerializeField] GameObject notReadyBtn;
    [SerializeField] Rumbler rumbler;


    public void Initialize(InputDevice device)
    {
        this.device = device;
        //var test = Gamepad.all.FirstOrDefault(g => g.deviceId == device.deviceId);
        //if (test != null)
        //    test.SetMotorSpeeds(0.123f, 0.234f);
        //rumbler.RumblePulse(0.2f, 0.4f, 0.25f, 2);
        //rumbler.Initialize(device);
        //rumbler.RumbleConstant(0.123f, 0.234f, 2);
        //StartCoroutine(Rumble(0.123f, 0.234f, 2));
        rumbler.SetGamepad(device);
        rumbler.Constant(0.123f, 0.234f, 0.2f);
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
