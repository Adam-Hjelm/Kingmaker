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

//public enum RumblePattern
//{
//    Constant,
//    Pulse,
//    Linear
//}

//public class Rumbler : MonoBehaviour
//{
//    [SerializeField] PlayerInput _playerInput;
//    //private RumblePattern activeRumblePattern;
//    private float rumbleDuration;
//    private float pulseDuration;
//    private float lowA;
//    private float lowStep;
//    private float highA;
//    private float highStep;
//    private float rumbleStep;
//    //private bool isMotorActive = false;
//    private Gamepad gamepad;
//    //private Coroutine currentCoroutine;


//    // Unity MonoBehaviors
//    private void Awake()
//    {
//        if (_playerInput == null) // If we already assigned a PlayerInput we shouldn't replace that reference.
//            _playerInput = GetComponent<PlayerInput>();
//    }

//    private void Start()
//    {
//        if (_playerInput != null)
//            gamepad = Gamepad.all.FirstOrDefault(g => _playerInput.devices.Any(d => d.deviceId == g.deviceId));
//    }

//    public void Initialize(InputDevice device = null, PlayerInput playerInput = null)
//    {
//        if (device != null)
//            gamepad = GetGamepad(device);
//        else if (playerInput != null)
//            gamepad = GetGamepad(playerInput);

//        if (gamepad == null)
//            Debug.LogError("Gamepad is null!");
//    }

//    public void StopRumble()
//    {
//        if (gamepad != null)
//            gamepad.SetMotorSpeeds(0, 0);
//    }

//    // Public Methods
//    public void RumbleConstant(float lowFrequency, float highFrequency, float durationSeconds)
//    {
//        if (gamepad == null)
//            Debug.LogError("Gamepad is null!");
//        //StopAllCoroutines();
//        //activeRumblePattern = RumblePattern.Constant;
//        //StopRumble();
//        //lowA = lowFrequency;
//        //highA = highFrequency;
//        //rumbleDuration = Time.time + durationSeconds;
//        StartCoroutine(RumbleConstantCoroutine(lowFrequency, highFrequency, durationSeconds));
//    }

//    public IEnumerator RumbleConstantCoroutine(float lowFrequency, float highFrequency, float durationSeconds)
//    {
//        StopRumble();

//        if (gamepad != null)
//        {
//            gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
//            yield return new WaitForSeconds(durationSeconds);
//            StopRumble();
//        }
//    }

//    public void RumblePulse(float lowFrequency, float highFrequency, float burstTime, int totalPulses)
//    {
//        StopAllCoroutines();
//        //activeRumblePattern = RumblePattern.Pulse;
//        //lowA = lowFrequency;
//        //highA = highFrequency;
//        rumbleStep = burstTime;
//        pulseDuration = Time.time + burstTime;
//        //rumbleDuration = Time.time + duration;
//        //isMotorActive = true;
//        //var g = GetGamepad();
//        //g?.SetMotorSpeeds(lowA, highA);
//        StartCoroutine(RumblePulseCoroutine(lowFrequency, highFrequency, totalPulses));
//    }

//    private IEnumerator RumblePulseCoroutine(float lowFrequency, float highFrequency, int totalPulses = 3)
//    {
//        StopRumble();

//        if (gamepad != null)
//        {
//            int currentPulse = 0;
//            bool isMotorActive = false;

//            while (currentPulse < totalPulses)
//            {
//                isMotorActive = !isMotorActive;
//                //pulseDuration = Time.time + rumbleStep;
//                if (!isMotorActive)
//                {
//                    gamepad.SetMotorSpeeds(0, 0);
//                }
//                else
//                {
//                    gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
//                }

//                yield return new WaitForSecondsRealtime(pulseDuration);
//                currentPulse++;
//            }

//            StopRumble();
//        }
//    }

//    public void RumbleLinear(float lowStart, float lowEnd, float highStart, float highEnd, float durationSeconds)
//    {
//        //activeRumblePattern = RumblePattern.Linear;
//        lowA = lowStart;
//        highA = highStart;
//        lowStep = (lowEnd - lowStart) / durationSeconds;
//        highStep = (highEnd - highStart) / durationSeconds;
//        rumbleDuration = Time.time + durationSeconds;
//        StartCoroutine(RumbleLinearCoroutine(durationSeconds));
//    }

//    private IEnumerator RumbleLinearCoroutine(float durationSeconds = 3)
//    {
//        StopRumble();

//        if (gamepad != null)
//        {
//            float _duration = Time.time + durationSeconds;

//            while (Time.time < _duration)
//            {
//                gamepad.SetMotorSpeeds(lowA, highA);
//                lowA += (lowStep * Time.deltaTime);
//                highA += (highStep * Time.deltaTime);
//                yield return null;
//            }

//            StopRumble();
//        }
//    }

//    //private void Update()
//    //{
//    //    if (_playerInput != null && gamepad != null)
//    //    {
//    //        if (Time.time > rumbleDuration)
//    //        {
//    //            StopRumble();
//    //            return;
//    //        }

//    //        //var gamepad = GetGamepad();
//    //        if (gamepad == null)
//    //            return;

//    //        switch (activeRumblePattern)
//    //        {
//    //            case RumblePattern.Constant:
//    //                gamepad.SetMotorSpeeds(lowA, highA);
//    //                break;

//    //            case RumblePattern.Pulse:

//    //                if(Time.time > pulseDuration)
//    //                {
//    //                    isMotorActive = !isMotorActive;
//    //                    pulseDuration = Time.time + rumbleStep;
//    //                    if (!isMotorActive)
//    //                    {
//    //                        gamepad.SetMotorSpeeds(0, 0);
//    //                    }
//    //                    else
//    //                    {
//    //                        gamepad.SetMotorSpeeds(lowA, highA);
//    //                    }
//    //                }

//    //                break;
//    //            case RumblePattern.Linear:
//    //                gamepad.SetMotorSpeeds(lowA, highA);
//    //                lowA += (lowStep * Time.deltaTime);
//    //                highA += (highStep * Time.deltaTime);
//    //                break;
//    //            default:
//    //                break;
//    //        }
//    //    }
//    //}

//    private void OnDestroy()
//    {
//        StopAllCoroutines();
//        StopRumble();
//    }

//    public Gamepad GetGamepad(PlayerInput playerInput)
//    {
//        if (playerInput != null)
//            return Gamepad.all.FirstOrDefault(g => playerInput.devices.Any(d => d.deviceId == g.deviceId));
//        else
//            return null;
//    }

//    public Gamepad GetGamepad(InputDevice device)
//    {
//        if (device != null)
//            return Gamepad.all.FirstOrDefault(g => device.deviceId == g.deviceId);
//        else
//            return null;
//    }
//}