using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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


    private static GameStates _gameState = GameStates.MainMenu;
    public static GameStates GameState { get => _gameState; }

    public int currentRound = 0;
    public int playersConnected = 0;
    public const int maxScoreToWin = 3; // placeholder value
    public const int maxNumberOfRounds = 7; // placeholder value

    [SerializeField] GameObject currentUpgradeScreen;
    public GameObject upgradeScreen;
    public GameObject gameScene;
    public GameObject lastPlayer;

    [SerializeField] List<PlayerInstance> players = new List<PlayerInstance>();
    [SerializeField] CanvasHandler canvasHandler;

    [SerializeField] Transform player1SpawnPos;
    [SerializeField] Transform player2SpawnPos;
    [SerializeField] Transform player3SpawnPos;
    [SerializeField] Transform player4SpawnPos;

    [SerializeField] GameObject player1Prefab;
    [SerializeField] GameObject player2Prefab;
    [SerializeField] GameObject player3Prefab;
    [SerializeField] GameObject player4Prefab;

    PlayerInputManager pim;


    void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;
    }

    void Start()
    {
        canvasHandler = FindObjectOfType<CanvasHandler>();
        canvasHandler.RoundText = $"Round: {currentRound}";

        gameScene = GameObject.Find("GameScene");

        pim = PlayerInputManager.instance;
        if (player1Prefab != null)
            pim.playerPrefab = player1Prefab;
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
                return "Purple Player";
            default:
                return "TOO MANY PLAYERS";
        }
    }

    public void KillPlayer(GameObject playerObject)
    {
        var player = players.FirstOrDefault(p => p.gameObject == playerObject);

        if (player != null)
        {
            player.isAlive = false;
            PlayerEnabled(false, player.gameObject);
            //player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            CheckWin(player);
        }
    }

    private void PlayerEnabled(bool isDisabled, GameObject playerToEnable)
    {
        playerToEnable.GetComponent<SpriteRenderer>().enabled = isDisabled;
        playerToEnable.GetComponent<Collider2D>().enabled = isDisabled;
        playerToEnable.GetComponent<PlayerMovement>().enabled = isDisabled;
    }

    /// <summary>
    /// Checks if a player has won or if a new round should be started.
    /// </summary>
    private void CheckWin(PlayerInstance lastKilledPlayer)
    {
        var livingPlayers = players.Where(p => p.isAlive).ToList();

        if (livingPlayers.Count() == 1)
        {
            livingPlayers[0].score++;
            lastPlayer = livingPlayers[0].gameObject;

        }
        else if (livingPlayers.Count() < 1)
            lastKilledPlayer.score++;
        else
            return; // do this to avoid the win condition check below

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
        gameScene.SetActive(false);

        PlayerEnabled(false, lastPlayer);

        GameObject upgradeScreenInstance = Instantiate(upgradeScreen, transform.position, Quaternion.identity);
        upgradeScreenInstance = currentUpgradeScreen;
    }

    private void Update()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
    }

    public void FinishedUpgrade()
    {

        //PlayerEnabled(true, lastPlayer);
        Debug.Log("UPGRADE DONE");
        canvasHandler.StartNewRound();

        foreach (var player in players)
        {
            player.controller.SetPlayerHealthToMax();
        }

        ResetScene();


    }

    /// <summary>
    /// Sets all gameobjects in the scene to their initial state. Excluding objects like GameManager that keeps score and stats.
    /// </summary>
    public void ResetScene()
    {
        gameScene.SetActive(true);

        Debug.Log("RESETTING SCENE");
        canvasHandler.RoundText = $"Round: {currentRound}";

        foreach (var player in players)
        {
            RespawnPlayer(player);

            player.isAlive = true;
            player.gameObject.SetActive(true);
            player.controller.currentHealth = player.controller.maxHealth;
            canvasHandler.UpdateScore(player.ID, player.score);
        }
    }

    private void RespawnPlayer(PlayerInstance player)
    {
        switch (player.ID)
        {
            case 0:
                player.gameObject.transform.position = player1SpawnPos.position;
                PlayerEnabled(true, player.gameObject);
                break;
            case 1:
                player.gameObject.transform.position = player2SpawnPos.position;
                PlayerEnabled(true, player.gameObject);
                break;
            case 2:
                player.gameObject.transform.position = player3SpawnPos.position;
                PlayerEnabled(true, player.gameObject);
                break;
            case 3:
            default:
                player.gameObject.transform.position = player4SpawnPos.position;
                PlayerEnabled(true, player.gameObject);
                break;
        }
    }

    private void HandleWin(PlayerInstance winningPlayer)
    {
        canvasHandler.StartWinScreen(winningPlayer.name);
        //TODO: change states
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

    void OnPlayerJoined(PlayerInput player)
    {
        Debug.Log("ON PLAYER JOIN METHOD TRIGGERED!");
        Debug.Log(player.playerIndex);

        //playersConnected++;
        int playerNumber = player.playerIndex;

        //while (players.Any(p => p.ID == playerNumber))
        //{
        //    playerNumber++;
        //}
        PlayerInstance newPlayer = new PlayerInstance
        {
            ID = playerNumber,
            name = GetPlayerName(playerNumber),
            gameObject = player.gameObject,
            controller = player.GetComponent<PlayerController>(),
            score = 0,
            isAlive = true
        };
        players.Add(newPlayer);
        RespawnPlayer(newPlayer);

        canvasHandler.UpdateScore(playerNumber, 0);

        switch (playerNumber)
        {
            case 0:
                pim.playerPrefab = player2Prefab;
                break;
            case 1:
                pim.playerPrefab = player3Prefab;
                break;
            case 2:
                pim.playerPrefab = player4Prefab;
                break;
            case 3:
            default:
                pim.playerPrefab = player1Prefab;
                PlayerInputManager.instance.DisableJoining();
                break;
        }
    }

    private void OnPlayerLeft(PlayerInput player)
    {
        Debug.Log("ON PLAYER LEFT METHOD TRIGGERED!");

        PlayerInstance _player = players.FirstOrDefault(p => p.ID == player.playerIndex);

        if (player != null)
            players.Remove(_player);
    }

    //public void AddPlayer(int playerNumber, GameObject playerObject, PlayerController controller)
    //{
    //    if (players.Count >= 4)
    //        return;

    //    if (!players.Any(p => p.ID == playerNumber))
    //    {
    //        players.Add(new PlayerInstance
    //        {
    //            ID = playerNumber,
    //            name = GetPlayerName(playerNumber),
    //            gameObject = playerObject,
    //            controller = controller,
    //            score = 0,
    //            isAlive = true
    //        });
    //        canvasHandler.UpdateScore(playerNumber, 0);
    //    }
    //}

    //public void RemovePlayer(int playerNumber)
    //{
    //    PlayerInstance player = players.FirstOrDefault(p => p.ID == playerNumber);

    //    if (player != null)
    //        players.Remove(player);
    //}

    private class PlayerInstance
    {
        /// <summary>
        /// Same as player number
        /// </summary>
        public int ID;
        public string name;
        public GameObject gameObject;
        public PlayerController controller;
        public int score;
        public bool isAlive;
    }

    IEnumerable<GameObject> FindAllPrefabVariants(string parentAssetPath)
    {
        return FindAllPrefabVariants(AssetDatabase.LoadAssetAtPath<GameObject>(parentAssetPath));
    }

    IEnumerable<GameObject> FindAllPrefabVariants(GameObject parent)
    {
        return AssetDatabase.FindAssets("t:prefab").
            Select(AssetDatabase.GUIDToAssetPath).
            Select(AssetDatabase.LoadAssetAtPath<GameObject>).
            Where(go => go != null).
            Where(go => PrefabUtility.GetPrefabAssetType(go) == PrefabAssetType.Variant).
            Where(go => PrefabUtility.GetCorrespondingObjectFromSource(go) == parent);
    }
}

public enum GameStates
{
    MainMenu,
    SelectionScreen,
    Playing,
    RoundWinScreen,
    UpgradeScreen,
    GameWinScreen
}
