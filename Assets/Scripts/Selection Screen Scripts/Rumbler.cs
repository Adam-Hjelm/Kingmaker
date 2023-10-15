using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Haptics;
using UnityEngine.InputSystem.Layouts;


public class Rumbler : MonoBehaviour
{
    public Gamepad gamepad;

    private void Start()
    {
        var playerInput = gameObject.GetComponent<PlayerInput>();

        if (playerInput != null)
            gamepad = Gamepad.all.FirstOrDefault(g => g.deviceId == playerInput.devices.FirstOrDefault().deviceId);
    }

    public void SetGamepad(InputDevice device)
    {
        gamepad = Gamepad.all.FirstOrDefault(g => g.deviceId == device.deviceId);
    }

    public void Constant(float low, float high, float duration)
    {
        StartCoroutine(Constant(gamepad, low, high, duration));
    }

    private IEnumerator Constant(Gamepad gamepad, float low, float high, float duration)
    {
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(low, high);
            yield return new WaitForSeconds(duration);
            gamepad.SetMotorSpeeds(0, 0);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        if (gamepad != null)
            gamepad.SetMotorSpeeds(0, 0);
    }
}
