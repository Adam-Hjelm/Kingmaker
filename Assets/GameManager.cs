using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GameManager instance is null!");

            return _instance;
        }
    }
    public static GameStates GameState { get; private set; } = GameStates.SelectionScreen;

    public int currentRound = 0;
    public int playersConnected = 0;
    public int maxScoreToWin = 3; // placeholder value
    public int maxNumberOfRounds = 7; // placeholder value

    [SerializeField, Tooltip("Must exist in scene")] GameObject upgradeScreen;
    [SerializeField] GameObject gameScene;

    [SerializeField] List<PlayerInstance> players = new List<PlayerInstance>();
    [SerializeField] CanvasHandler canvasHandler;

    [SerializeField] GameObject destroyableWalls;
    DestroyableObject[] destroyableObject;
    public GameObject DeathAnimation;

    [SerializeField] UpgradeController upgradeController;
    [SerializeField] TextMeshProUGUI countdownText;

    [Header("Player Stuff")]
    [SerializeField] Transform player1SpawnPos;
    [SerializeField] Transform player2SpawnPos;
    [SerializeField] Transform player3SpawnPos;
    [SerializeField] Transform player4SpawnPos;

    [SerializeField] GameObject player1Prefab;
    [SerializeField] GameObject player2Prefab;
    [SerializeField] GameObject player3Prefab;
    [SerializeField] GameObject player4Prefab;

    [SerializeField] Sprite player1Sprite;
    [SerializeField] Sprite player2Sprite;
    [SerializeField] Sprite player3Sprite;
    [SerializeField] Sprite player4Sprite;

    GameObject lastPlayer;
    PlayerInputManager pim;
    Coroutine countDownRoutine;


    void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;
    }

    void Start()
    {
        pim = PlayerInputManager.instance;

        if (canvasHandler == null)
            canvasHandler = FindObjectOfType<CanvasHandler>();
        //canvasHandler.RoundText = $"Round: {currentRound}";

        if (gameScene == null)
            gameScene = GameObject.Find("GameScene");

        if (player1Prefab != null && pim != null)
            pim.playerPrefab = player1Prefab;
    }

    public void StartGame()
    {

        Debug.Log(countDownRoutine);
        if (countDownRoutine == null)
            countDownRoutine = StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        yield return null;
        yield return null;

        foreach (var player in players)
        {
            player.controller.SetPlayerEnabled(false);
            player.controller.spriteRenderer.enabled = true;
        }

        int timer = 3;
        countdownText.gameObject.SetActive(true);
        countdownText.text = $"GAME STARTS IN\n<b>{timer}</b>";
        yield return new WaitForSeconds(1);
        timer--;
        countdownText.text = $"GAME STARTS IN\n<b>{timer}</b>";
        yield return new WaitForSeconds(1);
        timer--;
        countdownText.text = $"GAME STARTS IN\n<b>{timer}</b>";
        yield return new WaitForSeconds(1);
        timer--;
        countdownText.text = $"GAME STARTS IN\n<b>{timer}</b>";
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false);

        foreach (var player in players)
        {
            player.controller.SetPlayerEnabled(true);
        }
    }

    public int GetPlayerScore(int playerToGetScoreFrom)
    {
        if (playerToGetScoreFrom < 5 && playerToGetScoreFrom > -1)
        {
            if (playerToGetScoreFrom >= 0 && playerToGetScoreFrom < players.Count)
            {
                return players[playerToGetScoreFrom].score;
            }
            else
            {
                return 69420;
            }
        }
        else
        {
            return 69420;
        }
    }

    public PlayerInput GetPlayerInput(int ID)
    {
        var player = players.FirstOrDefault(p => p.ID == ID);
        return player?.playerInput;
    }

    private string GetPlayerName(int playerNumber)
    {
        switch (playerNumber)
        {
            case 0:
                return "Red Player";
            case 1:
                return "Blue Player";
            case 2:
                return "Green Player";
            case 3:
                return "Pink Player";
            default:
                Debug.LogError("TOO MANY PLAYERS");
                return "TOO MANY PLAYERS";
        }
    }

    public void KillPlayer(GameObject playerObject)
    {
        var player = players.FirstOrDefault(p => p.gameObject == playerObject);

        if (player != null)
        {
            player.isAlive = false;
            PlayerEnabled(false, player.controller);
            CheckRoundOver(player);
        }
    }

    private void PlayerEnabled(bool enabled, PlayerController playerController)
    {
        playerController.SetPlayerEnabled(enabled);
    }

    /// <summary>
    /// Checks if a player has won or if a new round should be started.
    /// </summary>
    private void CheckRoundOver(PlayerInstance lastKilledPlayer)
    {
        var livingPlayers = players.Where(p => p.isAlive).ToList();

        DeathAnimation = GameObject.FindGameObjectWithTag("DeathAnimation");
        Destroy(DeathAnimation,2.8f);

        if (livingPlayers.Count() == 1)
        {
            livingPlayers[0].score++;
            lastPlayer = livingPlayers[0].gameObject;

        }
        else if (livingPlayers.Count() < 1)
            lastKilledPlayer.score++;
        else
            return; // do this to avoid the win conditions check below

        var leadingPlayer = players.OrderByDescending(p => p.score).FirstOrDefault();

        if (leadingPlayer.score >= maxScoreToWin || currentRound >= maxNumberOfRounds)
            HandleWin(leadingPlayer);
        else
            StartCoroutine(HandleNewRound(livingPlayers[0].name));
    }

    private IEnumerator HandleNewRound(string playerName)
    {
        currentRound++;
        canvasHandler.StartWinRoundScreen(playerName);

        yield return new WaitForSeconds(3);

        StartUpgradeScreen();
    }

    private void StartUpgradeScreen()
    {
        GameState = GameStates.UpgradeScreen;
        gameScene.SetActive(false);

        foreach (var player in players)
        {
            player.controller.playerWon = false;
            player.playerInput.SwitchCurrentActionMap("UpgradeMenu");
        }
        var controller = lastPlayer.GetComponent<PlayerController>();
        controller.playerWon = true;
        PlayerEnabled(false, controller);
        controller.roundOver = true;
        lastPlayer.GetComponentInChildren<Canvas>().enabled = false;

        upgradeScreen.gameObject.SetActive(true);
    }

    public void FinishedUpgrade()
    {
        Debug.Log("UPGRADE DONE");
        lastPlayer.GetComponent<PlayerController>().roundOver = false;
        lastPlayer.GetComponentInChildren<Canvas>().enabled = true;

        foreach (var player in players)
        {
            player.controller.SetPlayerHealthToMax();
            player.playerInput.SwitchCurrentActionMap("Player");
        }
        upgradeScreen.gameObject.SetActive(false);
        ResetScene();
    }

    /// <summary>
    /// Sets all gameobjects in the scene to their initial state. Excluding objects like GameManager that keeps score and stats.
    /// </summary>
    public void ResetScene()
    {
        Debug.Log("RESETTING SCENE");
        gameScene.SetActive(true);



        destroyableObject = destroyableWalls.GetComponentsInChildren<DestroyableObject>();
        foreach (DestroyableObject wallScript in destroyableObject)
        {
            wallScript.timesHit = 6;
            wallScript.col.enabled = true;
            wallScript.GetComponent<SpriteRenderer>().sprite = wallScript.sprite1;
        }
        Debug.Log("resetting for reals");
        canvasHandler.StartNewRound();
        canvasHandler.RoundText = $"Round: {currentRound}";

        foreach (var player in players)
        {
            RespawnPlayer(player);

            player.isAlive = true;
            player.gameObject.SetActive(true);
            player.controller.currentHealth = player.controller.maxHealth;
            canvasHandler.UpdateScore(player.ID, player.score);
        }

        if (countDownRoutine != null)
            StopCoroutine(countDownRoutine);
        countDownRoutine = StartCoroutine(StartCountdown());
    }

    private void RespawnPlayer(PlayerInstance player)
    {
        switch (player.ID)
        {
            case 0:
                player.gameObject.transform.position = player1SpawnPos.position;
                break;
            case 1:
                player.gameObject.transform.position = player2SpawnPos.position;
                break;
            case 2:
                player.gameObject.transform.position = player3SpawnPos.position;
                break;
            case 3:
            default:
                player.gameObject.transform.position = player4SpawnPos.position;
                break;
        }
        PlayerEnabled(true, player.controller);
        //player.gameObject.GetComponentInChildren<Canvas>().enabled = true;

    }

    private void HandleWin(PlayerInstance winningPlayer)
    {
        //foreach (var player in players)
        //{
        //}
        destroyableWalls.SetActive(false);
        //winningPlayer.gameObject.GetComponent<SpriteRenderer>().enabled = false;/*.SetActive(false);*/
        PlayerEnabled(false, winningPlayer.controller);
        canvasHandler.StartWinScreen(winningPlayer.ID, winningPlayer.name, winningPlayer.gameObject, winningPlayer.sprite);
        //TODO: change states
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("ON PLAYER JOIN METHOD TRIGGERED! " + playerInput.playerIndex);

        playersConnected++;
        int playerNumber = playerInput.playerIndex;

        PlayerInstance player = new PlayerInstance
        {
            ID = playerNumber,
            name = GetPlayerName(playerNumber),
            gameObject = playerInput.gameObject,
            controller = playerInput.GetComponent<PlayerController>(),
            playerInput = playerInput,
            score = 0,
            isAlive = true
        };
        Debug.Log("ID = " + player.ID);
        Debug.Log(player.sprite);
        switch (player.ID) // placeholder
        {
            case 0:
                player.sprite = player1Sprite;
                break;
            case 1:
                player.sprite = player2Sprite;
                break;
            case 2:
                player.sprite = player3Sprite;
                break;
            case 3:
                player.sprite = player4Sprite;
                break;
        }
        Debug.Log(player.sprite);
        players.Add(player);
        RespawnPlayer(player);

        //canvasHandler.UpdateScore(playerNumber, 0);
        var multiplayerEventSystem = player.gameObject.GetComponentInChildren<MultiplayerEventSystem>();

        switch (playerNumber)
        {
            case 0:
                pim.playerPrefab = player2Prefab;
                upgradeController.playerEventSys1 = multiplayerEventSystem;
                break;
            case 1:
                pim.playerPrefab = player3Prefab;
                upgradeController.playerEventSys2 = multiplayerEventSystem;
                break;
            case 2:
                pim.playerPrefab = player4Prefab;
                upgradeController.playerEventSys3 = multiplayerEventSystem;
                break;
            case 3:
            default:
                pim.playerPrefab = player1Prefab;
                upgradeController.playerEventSys4 = multiplayerEventSystem;
                break;
        }

        //player.playerInput.SwitchCurrentActionMap("SelectionScreen");

        if (players.Count >= pim.maxPlayerCount)
            pim.DisableJoining();
    }

    public void OnPlayerLeft(PlayerInput player)
    {
        Debug.Log("ON PLAYER LEFT METHOD TRIGGERED!");

        PlayerInstance _player = players.FirstOrDefault(p => p.ID == player.playerIndex);

        if (player != null)
        {
            players.Remove(_player);
            playersConnected--;

            if (players.Count < pim.maxPlayerCount)
                pim.EnableJoining();
        }
    }

    #region scene functions

    public void StartNewGame()
    {
        SceneManager.LoadScene(0);

        currentRound = 0;

        //foreach (var player in players)
        //{
        //    player.score = 0;
        //    player.isAlive = true;
        //    player.gameObject.SetActive(true);
        //}

        //TODO: Reset the players stats to default, or just load the game scene and auto assign back players to the same characters.
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    #endregion

    private class PlayerInstance
    {
        public int ID;
        public string name;
        public GameObject gameObject;
        public PlayerController controller;
        public PlayerInput playerInput;
        public int score;
        public bool isAlive;
        public Sprite sprite;
    }

    //IEnumerable<GameObject> FindAllPrefabVariants(string parentAssetPath)
    //{
    //    return FindAllPrefabVariants(AssetDatabase.LoadAssetAtPath<GameObject>(parentAssetPath));
    //}

    //IEnumerable<GameObject> FindAllPrefabVariants(GameObject parent)
    //{
    //    return AssetDatabase.FindAssets("t:prefab").
    //        Select(AssetDatabase.GUIDToAssetPath).
    //        Select(AssetDatabase.LoadAssetAtPath<GameObject>).
    //        Where(go => go != null).
    //        Where(go => PrefabUtility.GetPrefabAssetType(go) == PrefabAssetType.Variant).
    //        Where(go => PrefabUtility.GetCorrespondingObjectFromSource(go) == parent);
    //}
}

public enum GameStates
{
    SelectionScreen,
    Playing,
    UpgradeScreen,
    GameWinScreen
}
