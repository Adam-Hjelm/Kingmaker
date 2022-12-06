using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionScreen : MonoBehaviour
{
    #region DO NOT TOUCH!
    // WARNING!
    // This part is taken from PlayerInputManager.cs source code and it works. If you get errors from here something else is wrong.
    // Contact Kevin if you get stuck here.
    // DO NOT MODIFY THIS REGION!


    [SerializeField] internal bool m_AllowJoining = true;

    [NonSerialized] private bool m_JoinActionDelegateHooked;
    [NonSerialized] private Action<InputAction.CallbackContext> m_JoinActionDelegate;
    [NonSerialized] private bool m_LeaveActionDelegateHooked;
    [NonSerialized] private Action<InputAction.CallbackContext> m_LeaveActionDelegate;

    [SerializeField] internal InputActionProperty m_JoinAction;
    [SerializeField] internal InputActionProperty m_LeaveAction;
    //public InputActionProperty JoinAction
    //{
    //    get => m_JoinAction;
    //    set
    //    {
    //        if (m_JoinAction == value)
    //            return;

    //        if (m_AllowJoining)
    //            DisableJoining();

    //        m_JoinAction = value;

    //        if (m_AllowJoining)
    //            EnableJoining();
    //    }
    //}

    //public void EnableJoining()
    //{
    //    if (m_JoinAction.action != null)
    //    {
    //        if (!m_JoinActionDelegateHooked)
    //        {
    //            if (m_JoinActionDelegate == null)
    //                m_JoinActionDelegate = PlayerJoin;

    //            m_JoinAction.action.performed += m_JoinActionDelegate;
    //            m_JoinActionDelegateHooked = true;
    //        }
    //        m_JoinAction.action.Enable();
    //    }
    //    else
    //    {
    //        Debug.LogError("No join action configured on SelectionScreen. Set an action before proceeding!", this);
    //    }
    //}

    //public void DisableJoining()
    //{
    //    if (m_JoinActionDelegateHooked)
    //    {
    //        if (m_JoinAction.action != null)
    //            m_JoinAction.action.performed -= m_JoinActionDelegate;
    //        m_JoinActionDelegateHooked = false;
    //    }
    //    m_JoinAction.action?.Disable();
    //}

    private void OnEnable()
    {
        // if the join action is a reference, clone it so we don't run into problems with the action being disabled by
        // PlayerInput when devices are assigned to individual players
        //if (m_JoinAction.reference != null && m_JoinAction.action?.actionMap?.asset != null)
        //{
        //    var inputActionAsset = Instantiate(m_JoinAction.action.actionMap.asset);
        //    var inputActionReference = InputActionReference.Create(inputActionAsset.FindAction(m_JoinAction.action.name));
        //    m_JoinAction = new InputActionProperty(inputActionReference);
            
        //    //DisableJoining();
        //    //EnableJoining();

        //}

        m_JoinActionDelegate = JoinPlayer;
        m_LeaveActionDelegate = RemovePlayer;

        RemoveActionDelegates();

        m_JoinAction.action.performed += m_JoinActionDelegate;
        m_JoinActionDelegateHooked = true;
        m_JoinAction.action.Enable();

        m_LeaveAction.action.performed += m_LeaveActionDelegate;
        m_LeaveActionDelegateHooked = true;
        m_LeaveAction.action.Enable();
    }

    private void OnDisable()
    {
        RemoveActionDelegates();
        //if (m_AllowJoining)
        //    DisableJoining();
        //if (m_JoinActionDelegateHooked)
        //{
        //    if (m_JoinAction.action != null)
        //        m_JoinAction.action.performed -= m_JoinActionDelegate;
        //}
        //DisableJoining();
    }

    private void RemoveActionDelegates()
    {
        if (m_JoinActionDelegateHooked)
        {
            m_JoinAction.action.performed -= m_JoinActionDelegate;
            m_JoinActionDelegateHooked = false;
            m_JoinAction.action?.Disable();
        }

        if (m_LeaveActionDelegateHooked)
        {
            m_LeaveAction.action.performed -= m_LeaveActionDelegate;
            m_LeaveActionDelegateHooked = false;
            m_LeaveAction.action?.Disable();
        }
    }

    #endregion


    [SerializeField] int totalPlayers = 0;
    [SerializeField] int maxAllowedPlayers = 4;
    List<PlayerObject> players = new List<PlayerObject>();

    [SerializeField] GameObject playerCardPrefab;
    [SerializeField] GameObject canvasObject;

    //Vector3 pos = new Vector3(-280, 9, 0);
    [SerializeField] Transform spawnPos1;
    [SerializeField] Transform spawnPos2;
    [SerializeField] Transform spawnPos3;
    [SerializeField] Transform spawnPos4;

    [SerializeField] GameObject playerCard1;
    [SerializeField] GameObject playerCard2;
    [SerializeField] GameObject playerCard3;
    [SerializeField] GameObject playerCard4;


    //public void Start()
    //{
        
    //}

    public void JoinPlayer(InputAction.CallbackContext context)
    {
        Debug.Log("JoinPlayer method called");

        if (!m_AllowJoining || totalPlayers >= maxAllowedPlayers)
            return;

        var device = context.control.device;

        if (players.Any(p => p.device == device))
            return;

        //if (PlayerInput.FindFirstPairedToDevice(device) != null)
        //    return;

        totalPlayers++;

        GameObject playerCard;

        if (playerCard1 == null)
        {
            playerCard = Instantiate(playerCardPrefab, spawnPos1.position, spawnPos1.rotation, canvasObject.transform);
            playerCard1 = playerCard;
        }
        else if (playerCard2 == null)
        {
            playerCard = Instantiate(playerCardPrefab, spawnPos2.position, spawnPos2.rotation, canvasObject.transform);
            playerCard2 = playerCard;
        }
        else if (playerCard3 == null)
        {
            playerCard = Instantiate(playerCardPrefab, spawnPos3.position, spawnPos3.rotation, canvasObject.transform);
            playerCard3 = playerCard;
        }
        else if (playerCard4 == null)
        {
            playerCard = Instantiate(playerCardPrefab, spawnPos4.position, spawnPos4.rotation, canvasObject.transform);
            playerCard4 = playerCard;
        }
        else
        {
            Debug.LogError("JoinPlayer method called but player count is already at maximum");
            return;
        }

        //playerCard.transform.localPosition = pos;
        //pos += Vector3.right * 210;
        playerCard.SetActive(true);

        TextMeshProUGUI playerName = playerCard.transform.Find("user text").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI playerInfo = playerCard.transform.Find("user info").GetComponent<TextMeshProUGUI>();

        playerName.text = device.displayName;
        playerInfo.text = $"name: '{device.name}'\ndisplayName: '{device.displayName}'\ndeviceId: '{device.deviceId}'\nnative: '{device.native}'";

        players.Add(new PlayerObject
        {
            device = device,
            card = playerCard,
            playerID = totalPlayers
        });

        if (totalPlayers >= maxAllowedPlayers)
            m_AllowJoining = false;

        //OnPlayerJoined(device);
        //PlayerInputManager.instance.JoinPlayer(pairWithDevice: device);
    }

    public void RemovePlayer(InputAction.CallbackContext context)
    {
        Debug.Log("RemovePlayer method called");

        if (totalPlayers <= 0)
            return;

        var device = context.control.device;
        var player = players.FirstOrDefault(p => p.device == device);

        if (player == null)
            return;

        players.Remove(player);
        Destroy(player.card);
        totalPlayers--;

        if (totalPlayers < maxAllowedPlayers)
            m_AllowJoining = true;
    }

    //public void OnPlayerJoined(InputDevice device)
    //{
    //    var playerCard = Instantiate(playerCardPrefab, canvasObject.transform);
    //    playerCard.transform.localPosition = pos;
    //    pos += Vector3.right * 210;
    //    playerCard.SetActive(true);
    //    TextMeshProUGUI playerName = playerCard.transform.Find("user text").GetComponent<TextMeshProUGUI>();
    //    TextMeshProUGUI playerInfo = playerCard.transform.Find("user info").GetComponent<TextMeshProUGUI>();
    //    playerName.text = totalPlayers.ToString();
    //    playerInfo.text = $"name: '{device.name}'\ndisplayName: '{device.displayName}'\ndeviceId: '{device.deviceId}'\nnative: '{device.native}'";

    //    players.Add(new PlayerObject
    //    {
    //        device = device,
    //        card = playerCard,
    //        playerID = totalPlayers
    //    });

    //    if (totalPlayers >= maxAllowedPlayers)
    //        m_AllowJoining = false;
    //}

    //public void OnPlayerLeft(InputAction.CallbackContext context)
    //{
    //    var device = context.control.device;

    //    if (players.Any(p => p.device == device))
    //        return;

    //    var player = players.FirstOrDefault(p => p.device == device);
    //    if (player != null)
    //    {
    //        players.Remove(player);
    //        Destroy(player.card);
    //        totalPlayers--;

    //        if (totalPlayers < maxAllowedPlayers)
    //            m_AllowJoining = true;
    //    }
    //}

    private class PlayerObject
    {
        public InputDevice device { get; set; }
        public GameObject card { get; set; }
        public int playerID { get; set; }
    }
}
