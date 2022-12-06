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


    public static GameStates GameState { get; private set; } = GameStates.SelectionScreen;

    public int currentRound = 0;
    public int playersConnected = 0;
    public const int maxScoreToWin = 3; // placeholder value
    public const int maxNumberOfRounds = 7; // placeholder value

    [SerializeField, Tooltip("Must exist in scene")] UpgradeManager upgradeScreen;
    [SerializeField] GameObject gameScene;

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

    [SerializeField] DragDropScript playerDragDrop1;
    [SerializeField] DragDropScript playerDragDrop2;
    [SerializeField] DragDropScript playerDragDrop3;
    [SerializeField] DragDropScript playerDragDrop4;

    GameObject lastPlayer;
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
        pim = PlayerInputManager.instance;

        if (canvasHandler == null)
            canvasHandler = FindObjectOfType<CanvasHandler>();
        //canvasHandler.RoundText = $"Round: {currentRound}";

        if (gameScene == null)
            gameScene = GameObject.Find("GameScene");

        if (player1Prefab != null && pim != null)
            pim.playerPrefab = player1Prefab;
    }

    public PlayerInput GetPlayerInput(int ID)
    {
        var player = players.FirstOrDefault(p => p.ID == ID);
        return player != null ? player.playerInput : null;
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
            CheckRoundOver(player);
        }
    }

    private void PlayerEnabled(bool enabled, GameObject playerObject)
    {
        playerObject.GetComponent<SpriteRenderer>().enabled = enabled;
        playerObject.GetComponent<Collider2D>().enabled = enabled;
        //playerObject.GetComponent<PlayerMovement>().enabled = enabled;
    }

    /// <summary>
    /// Checks if a player has won or if a new round should be started.
    /// </summary>
    private void CheckRoundOver(PlayerInstance lastKilledPlayer)
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

        PlayerEnabled(false, lastPlayer);
        foreach (var player in players)
        {
            player.playerInput.SwitchCurrentActionMap("UpgradeMenu");
        }

        upgradeScreen.gameObject.SetActive(true);
    }

    public void FinishedUpgrade()
    {
        Debug.Log("UPGRADE DONE");
        

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

        PlayerEnabled(true, player.gameObject);
    }

    private void HandleWin(PlayerInstance winningPlayer)
    {
        canvasHandler.StartWinScreen(winningPlayer.name);
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

        players.Add(player);
        //RespawnPlayer(player);

        //canvasHandler.UpdateScore(playerNumber, 0);

        switch (playerNumber)
        {
            case 0:
                pim.playerPrefab = player2Prefab;
                //player.controller.dragDropPlayer = playerDragDrop1;
                break;
            case 1:
                pim.playerPrefab = player3Prefab;
                //player.controller.dragDropPlayer = playerDragDrop2;
                break;
            case 2:
                pim.playerPrefab = player4Prefab;
                //player.controller.dragDropPlayer = playerDragDrop3;
                break;
            case 3:
            default:
                pim.playerPrefab = player1Prefab;
                //player.controller.dragDropPlayer = playerDragDrop4;
                break;
        }

        player.playerInput.SwitchCurrentActionMap("SelectionScreen");

        if (players.Count >= pim.maxPlayerCount)
            pim.DisableJoining();
    }

    public void OnPlayerLeft(PlayerInput player)
    {
        pim.JoinPlayer();
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
