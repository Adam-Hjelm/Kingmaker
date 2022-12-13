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

public class SelectionScreen : MonoBehaviour
{
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
    

	private void Awake()
    {
        DontDestroyOnLoad(this);
    }

	private void OnEnable()
	{
        SceneManager.sceneLoaded += GameSceneLoaded;
        Debug.Log("sceneloaded delegate is set to gamesceneloaded method!");
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= GameSceneLoaded;
        if (PlayerInputManager.instance != null)
            PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
    }

    void Start()
    {
        if (PlayerInputManager.instance != null)
            PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("OnPlayerJoined() called!");
        //JoinPlayer(playerInput.gameObject);
        var device = playerInput.devices.FirstOrDefault();
        int ID = (int)playerInput.user.id;
		List<int> ids = players.Select(p => p.playerID).ToList();

        if (!allowJoining || totalPlayers >= maxAllowedPlayers)
            return;

        if (players.Any(p => p.device == device))
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
            playerCard1 = playerCard;
        }
		else if (playerCard2 == null)
		{
            SetPositionOfPlayerCard(playerCard, spawnPos2, playerSprites[1], "Blue Player");
            playerCard2 = playerCard;
        }
        else if (playerCard3 == null)
		{
			SetPositionOfPlayerCard(playerCard, spawnPos3, playerSprites[2], "Green Player");
            playerCard3 = playerCard;
		}
		else if (playerCard4 == null)
		{
            
            SetPositionOfPlayerCard(playerCard, spawnPos4, playerSprites[3], "Pink Player");
            playerCard4 = playerCard;
		}
		else
		{
			Debug.LogError("JoinPlayer method called but player count is already at maximum");
			return;
		}

		var player = new PlayerObject
        {
            device = device,
            card = playerCard,
            playerID = ID
        };

        players.Add(player);
        totalPlayers++;

		if (totalPlayers >= maxAllowedPlayers)
        {
			allowJoining = false;
            PlayerInputManager.instance.DisableJoining();
        }
    }

    private void OnPlayerLeft(PlayerInput playerInput)
    {
        Debug.Log("OnPlayerLeft() called!");
    }

    public void PlayerReady(InputAction input)
    {
        Debug.Log("OnReady() called!");
    }

    private void SetPositionOfPlayerCard(GameObject playerCard, Transform spawn, Sprite sprite, string playerName)
	{
        playerCard.transform.SetPositionAndRotation(spawn.position, spawn.rotation);
        playerCard.transform.Find("Character").GetComponent<Image>().sprite = sprite;
        playerCard.transform.Find("user text").GetComponent<TextMeshProUGUI>().text = playerName;
    }

	public void RemovePlayer(InputDevice device)
	{
		Debug.Log("RemovePlayer method called");

		if (totalPlayers <= 0)
			return;

		var player = players.FirstOrDefault(p => p.device == device);

		if (player == null)
			return;

		players.Remove(player);
		Destroy(player.card);

		totalPlayers--;
		Debug.Log("Player was removed!");

		if (totalPlayers < maxAllowedPlayers)
			allowJoining = true;
	}

    public void StartGame()
    {
        Debug.Log("pressed Start Game");
        if (PlayerInputManager.instance != null)
            PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
        StartCoroutine(DestroyPlayersAndInputManager());
    }

    private IEnumerator DestroyPlayersAndInputManager()
    {
        foreach (var player in players)
        {
            Destroy(player.card);
            yield return null;
        }
        yield return null;
        SceneManager.LoadScene(gameScene);
    }

    public void GameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log("LoadSceneMode: " + mode);
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

        players = players.OrderBy(p => p.playerID).ToList();

        foreach (var player in players)
        {
            PlayerInputManager.instance.JoinPlayer(playerIndex: player.playerID, pairWithDevice: player.device);
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
