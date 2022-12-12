using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    private void OnEnable()
    {
        m_JoinActionDelegate = JoinPlayer;
        m_LeaveActionDelegate = RemovePlayer;

        RemoveActionDelegates();

        m_JoinAction.action.performed += m_JoinActionDelegate;
        m_JoinActionDelegateHooked = true;
        m_JoinAction.action.Enable();

        m_LeaveAction.action.performed += m_LeaveActionDelegate;
        m_LeaveActionDelegateHooked = true;
        m_LeaveAction.action.Enable();

        SceneManager.sceneLoaded += GameSceneLoaded;
    }

    private void OnDisable()
    {
        RemoveActionDelegates();

        SceneManager.sceneLoaded -= GameSceneLoaded;
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


    public string gameScene = "GameScene";

    [SerializeField] int totalPlayers = 0;
    [SerializeField] int maxAllowedPlayers = 4;
    List<PlayerObject> players = new List<PlayerObject>();

    [SerializeField] GameObject playerCardPrefab;
    [SerializeField] GameObject canvasObject;

    [SerializeField] Transform spawnPos1;
    [SerializeField] Transform spawnPos2;
    [SerializeField] Transform spawnPos3;
    [SerializeField] Transform spawnPos4;

    [SerializeField] GameObject playerCard1;
    [SerializeField] GameObject playerCard2;
    [SerializeField] GameObject playerCard3;
    [SerializeField] GameObject playerCard4;


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

	//private void Update()
	//{
 //       if ((Gamepad.current != null && Gamepad.current.startButton.isPressed))
 //           //(Joystick.current != null && Joystick.current.))
 //       {
 //           Debug.Log("start was pressed");
 //           //SceneManager.LoadScene(gameScene);
 //           StartGame();
	//	}
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
        //TODO: Uncomment and fix the bug!
        //Debug.Log("RemovePlayer method called");

        //if (totalPlayers <= 0)
        //    return;

        //var device = context.control.device;
        //var player = players.FirstOrDefault(p => p.device == device);

        //if (player == null)
        //    return;

        //players.Remove(player);
        //Destroy(player.card);
        //totalPlayers--;

        //if (totalPlayers < maxAllowedPlayers)
        //    m_AllowJoining = true;
    }

    public void StartGame()
    {
        Debug.Log("pressed Start Game");
        SceneManager.LoadScene(gameScene);
    }

    public void GameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        if (scene.name == gameScene)
            StartCoroutine(InstantiatePlayers());
        
    }

    private IEnumerator InstantiatePlayers()
    {
        yield return null;

        var pim = PlayerInputManager.instance;

        players.OrderBy(p => p.playerID);
        foreach (var player in players)
        {
            pim.JoinPlayer(playerIndex: player.playerID, pairWithDevice: player.device);
        }

        Destroy(gameObject);
    }

    private class PlayerObject
    {
        public InputDevice device { get; set; }
        public GameObject card { get; set; }
        public int playerID { get; set; }
    }
}
