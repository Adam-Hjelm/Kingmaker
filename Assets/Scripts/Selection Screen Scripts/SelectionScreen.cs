using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SelectionScreen : MonoBehaviour
{
    private static SelectionScreen _instance;
    public static SelectionScreen Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("SelectionScreen instance is null!");

            return _instance;
        }
    }


    [SerializeField] private bool allowJoining = true;
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

    [SerializeField] List<Sprite> playerSprites = new List<Sprite>();
    [SerializeField] TextMeshProUGUI readyTimer;

    private int timeBeforeStartWhenReady = 3;
    private bool allPlayersReady = false;

    Coroutine startRoutine;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip soundEffect;


    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += GameSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= GameSceneLoaded;
        if (PlayerInputManager.instance != null)
            PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (PlayerInputManager.instance != null)
            PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        //JoinPlayer(playerInput.gameObject);
        var device = playerInput.devices.FirstOrDefault();
        int ID = playerInput.playerIndex;
        List<int> ids = players.Select(p => p.PlayerID).ToList();

        if (!allowJoining || totalPlayers >= maxAllowedPlayers)
            return;

        if (players.Any(p => p.Device == device))
            return;

        if (ids.Any(i => i == ID))
        {
            Debug.LogError("ID already exists!");
            return;
        }

        var playerCard = playerInput.gameObject;
        playerCard.transform.SetParent(canvasObject.transform, false);

        if (playerCard1 == null)
        {
            SetPositionOfPlayerCard(playerCard, spawnPos1, playerSprites[0], "Red Player");
            spawnPos1.GetChild(0).gameObject.SetActive(false);
            playerCard1 = playerCard;
        }
        else if (playerCard2 == null)
        {
            SetPositionOfPlayerCard(playerCard, spawnPos2, playerSprites[1], "Blue Player");
            spawnPos2.GetChild(0).gameObject.SetActive(false);
            playerCard2 = playerCard;
        }
        else if (playerCard3 == null)
        {
            SetPositionOfPlayerCard(playerCard, spawnPos3, playerSprites[2], "Green Player");
            spawnPos3.GetChild(0).gameObject.SetActive(false);
            playerCard3 = playerCard;
        }
        else if (playerCard4 == null)
        {
            SetPositionOfPlayerCard(playerCard, spawnPos4, playerSprites[3], "Pink Player");
            spawnPos4.GetChild(0).gameObject.SetActive(false);
            playerCard4 = playerCard;
        }
        else
        {
            Debug.LogError("JoinPlayer method called but player count is already at maximum");
            return;
        }

        //audioSource.PlayOneShot(soundEffect);
        playerCard.GetComponent<SelectionScreenPlayerCard>().Initialize(device);
        var player = new PlayerObject
        {
            Device = device,
            PlayerGameObject = playerCard,
            PlayerID = ID

        };

        players.Add(player);
        audioSource.PlayOneShot(soundEffect);
        totalPlayers++;
        StopTimer();

        if (totalPlayers >= maxAllowedPlayers)
        {
            allowJoining = false;
            PlayerInputManager.instance.DisableJoining();
        }
    }

    public void RemovePlayer(InputDevice device)
    {
        if (totalPlayers <= 0)
            return;

        var player = players.FirstOrDefault(p => p.Device == device);

        if (player == null)
            return;

        switch (player.PlayerID)
        {
            case 0:
                spawnPos1.GetChild(0).gameObject.SetActive(true);
                break;
            case 1:
                spawnPos2.GetChild(0).gameObject.SetActive(true);
                break;
            case 2:
                spawnPos3.GetChild(0).gameObject.SetActive(true);
                break;
            case 3:
                spawnPos4.GetChild(0).gameObject.SetActive(true);
                break;
        }

        players.Remove(player);
        Destroy(player.PlayerGameObject);

        totalPlayers--;

        if (allPlayersReady && totalPlayers >= 2)
        {
            StopTimer();
            StartTimer();
        }
        else
        {
            StopTimer();
        }

        if (totalPlayers < maxAllowedPlayers)
        {
            allowJoining = true;
            PlayerInputManager.instance.EnableJoining();
        }
    }

    private void SetPositionOfPlayerCard(GameObject playerCard, Transform spawn, Sprite sprite, string playerName)
    {
        playerCard.transform.SetPositionAndRotation(spawn.position, spawn.rotation);
        playerCard.transform.Find("Character").GetComponent<Image>().sprite = sprite;
        playerCard.transform.Find("user text").GetComponent<TextMeshProUGUI>().text = playerName;
    }

    public void PlayerChangedReadyState(InputDevice device, bool isReady)
    {
        if (!isReady)
            allPlayersReady = false;

        var player = players.FirstOrDefault(p => p.Device == device);
        if (player != null)
        {
            player.IsReady = isReady;

            if (players.Count >= 2 && players.All(p => p.IsReady))
                StartTimer();
            else if (startRoutine != null)
                StopTimer();
        }
    }

    private void StartTimer()
    {
        allPlayersReady = true;
        startRoutine = StartCoroutine(StartReadyTimer());
    }

    private void StopTimer()
    {
        allPlayersReady = false;
        readyTimer.gameObject.SetActive(false);

        if (startRoutine != null)
            StopCoroutine(startRoutine);
    }

    private IEnumerator StartReadyTimer()
    {
        readyTimer.gameObject.SetActive(true);

        int timer = timeBeforeStartWhenReady;
        while (timer >= 0 && allPlayersReady)
        {
            readyTimer.text = $"Game starts in {timer} seconds!";
            yield return new WaitForSeconds(1);
            timer--;
        }

        if (allPlayersReady)
            StartGame();
        else
            readyTimer.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        if (PlayerInputManager.instance != null)
            PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;

        if (startRoutine != null)
        {
            StopCoroutine(startRoutine);
        }

        StartCoroutine(DestroyPlayersAndInputManager());
    }

    // Do this in a coroutine to ensure nothing mess up when changing scenes
    private IEnumerator DestroyPlayersAndInputManager()
    {
        foreach (var player in players)
        {
            Destroy(player.PlayerGameObject);
            yield return null;
        }
        yield return null;
        SceneManager.LoadScene(gameScene);
    }

    public void GameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);
        if (scene.name == gameScene)
        {
            StartCoroutine(InstantiatePlayers());
        }
    }

    private IEnumerator InstantiatePlayers()
    {
        yield return null;
        yield return null;
        yield return null;

        players = players.OrderBy(p => p.PlayerID).ToList();

        foreach (var player in players)
        {
            PlayerInputManager.instance.JoinPlayer(playerIndex: player.PlayerID, pairWithDevice: player.Device);
        }

        if (GameManager.Instance == null)
            Debug.LogError("No instance of GameManager exists yet!");

        GameManager.Instance.StartGame();
        Destroy(gameObject);
    }

    private class PlayerObject
    {
        public int PlayerID { get; set; }
        public SelectionScreenPlayerCard PlayerCard { get; set; }
        public InputDevice Device { get; set; }
        public GameObject PlayerGameObject { get; set; }
        public bool IsReady { get; set; } = false;
    }
}
