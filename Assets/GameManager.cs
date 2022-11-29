using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
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

    public const int maxScoreToWin = 3; // placeholder value
    public const int maxNumberOfRounds = 7; // placeholder value

    [SerializeField] List<PlayerInstance> players = new List<PlayerInstance>();
    int currentRound = 0;
    [SerializeField] CanvasHandler canvasHandler;

    [SerializeField] Transform playerPos1;
    [SerializeField] Transform playerPos2;
    [SerializeField] Transform playerPos3;
    [SerializeField] Transform playerPos4;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        canvasHandler = FindObjectOfType<CanvasHandler>();
        canvasHandler.RoundText = $"Round: {currentRound}";
    }

    public void AddPlayer(int playerNumber, GameObject playerObject, PlayerController controller)
    {
        if (players.Count >= 4)
            return;

        if (!players.Any(p => p.ID == playerNumber))
        {
            players.Add(new PlayerInstance
            {
                ID = playerNumber,
                name = GetPlayerName(playerNumber),
                gameObject = playerObject,
                controller = controller,
                score = 0,
                isAlive = true
            });
            canvasHandler.UpdateScore(playerNumber, 0);
        }
    }

    public void RemovePlayer(int playerNumber)
    {
        PlayerInstance player = players.FirstOrDefault(p => p.ID == playerNumber);

        if (player != null)
            players.Remove(player);
    }

    private string GetPlayerName(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                return "Green Player";
            case 2:
                return "Red Player";
            case 3:
                return "Blue Player";
            case 4:
            default:
                return "Yellow Player";
        }
    }

    public void KillPlayer(int playerNumber)
    {
        var player = players.FirstOrDefault(p => p.ID == playerNumber);

        if (player != null)
        {
            player.isAlive = false;
            player.gameObject.SetActive(false);
            CheckWin(player);
        }
    }

    /// <summary>
    /// Checks if a player has won or if a new round should be started.
    /// </summary>
    private void CheckWin(PlayerInstance lastKilledPlayer)
    {
        var livingPlayers = players.Where(p => p.isAlive).ToList();

        if (livingPlayers.Count() == 1)
            livingPlayers[0].score++;
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

        canvasHandler.StartNewRound();

        foreach (var player in players)
        {
            player.controller.SetPlayerHealthToMax();
        }

        //TODO: go to upgrade screen

        ResetScene();
    }

    /// <summary>
    /// Sets all gameobjects in the scene to their initial state. Excluding objects like GameManager that keeps score and stats.
    /// </summary>
    public void ResetScene()
    {
        canvasHandler.RoundText = $"Round: {currentRound}";

        foreach (var player in players)
        {
            switch (player.ID)
            {
                case 1:
                    player.gameObject.transform.position = playerPos1.position;
                    break;
                case 2:
                    player.gameObject.transform.position = playerPos2.position;
                    break;
                case 3:
                    player.gameObject.transform.position = playerPos3.position;
                    break;
                case 4:
                default:
                    player.gameObject.transform.position = playerPos4.position;
                    break;
            }

            player.isAlive = true;
            player.gameObject.SetActive(true);
            player.controller.currentHealth = player.controller.maxHealth;
            canvasHandler.UpdateScore(player.ID, player.score);
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
        currentRound = 0;

        foreach (var player in players)
        {
            player.score = 0;
            player.isAlive = true;
            player.gameObject.SetActive(true);
        }

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
