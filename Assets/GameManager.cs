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
        canvasHandler.RoundText = $"Round: {currentRound}";
    }

    public void AddCanvasHandler(CanvasHandler canvasHandler)
    {
        this.canvasHandler = canvasHandler;
    }

    public void AddPlayer(int playerNumber, GameObject playerObject)
    {
        Debug.Log("added player: " + playerNumber);

        if (players.Count >= 4)
            return;

        if (!players.Any(p => p.ID == playerNumber))
        {
            players.Add(new PlayerInstance
            {
                ID = playerNumber,
                name = GetPlayerName(playerNumber),
                gameObject = playerObject,
                score = 0,
                isAlive = true
            });
        }
    }

    public void RemovePlayer(int playerNumber)
    {
        Debug.Log("removed player: " + playerNumber);

        PlayerInstance player = players.FirstOrDefault(p => p.ID == playerNumber);

        if (player != null)
            players.Remove(player);
    }

    private string GetPlayerName(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                return "Green";
            case 2:
                return "Red";
            case 3:
                return "Blue";
            case 4:
            default:
                return "Yellow";
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

    private void CheckWin(PlayerInstance lastKilledPlayer)
    {
        var livingPlayers = players.Where(p => p.isAlive).ToList();

        if (livingPlayers.Count() == 1)
            livingPlayers[0].score++;
        else if (livingPlayers.Count() < 1)
            lastKilledPlayer.score++;
        else
            return; // do this to avoid the win condition check below

        var leadingPlayer = players.OrderBy(p => p.score).FirstOrDefault();

        if (leadingPlayer.score >= maxScoreToWin || currentRound >= maxNumberOfRounds)
            HandleWin(leadingPlayer);
        else
            HandleNewRound();
    }

    private void HandleNewRound()
    {
        currentRound++;

        ResetScene();

        //foreach (var player in players)
        //{
        //    player.isAlive = true;
        //    player.gameObject.SetActive(true);
        //}

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //canvasHandler.RoundText = $"Round: {currentRound}";
        //TODO: go to upgrade screen
    }

    /// <summary>
    /// Sets all gameobjects in the scene to their initial state. Excluding objects like GameManager that keeps score and stats.
    /// </summary>
    public void ResetScene()
    {
        canvasHandler.RoundText = $"Round: {currentRound}";

        //TODO: player positions

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
        }

        //TODO: change state of UI
    }

    private void HandleWin(PlayerInstance winningPlayer)
    {
        canvasHandler.StartWinScreen(winningPlayer.name);
    }

    public void StartNewGame()
    {
        currentRound = 0;

        foreach (var player in players)
        {
            player.score = 0;
            player.isAlive = true;
            player.gameObject.SetActive(true);
        }

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {

    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }


    private class PlayerInstance
    {
        /// <summary>
        /// Same as player number
        /// </summary>
        public int ID;
        public string name;
        public GameObject gameObject;
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
