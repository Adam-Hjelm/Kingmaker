using TMPro;
using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
    public string RoundText { get => roundText.text; set => roundText.text = value; }
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI playerScore1;
    [SerializeField] TextMeshProUGUI playerScore2;
    [SerializeField] TextMeshProUGUI playerScore3;
    [SerializeField] TextMeshProUGUI playerScore4;

    [SerializeField] GameObject scoreScreen;
    [SerializeField] GameObject p1WinScreen;
    [SerializeField] GameObject p2WinScreen;
    [SerializeField] GameObject p3WinScreen;
    [SerializeField] GameObject p4WinScreen;
    [SerializeField] GameObject currentWinScreen;
    [SerializeField] TextMeshProUGUI winGameText;
    [SerializeField] TextMeshProUGUI winRoundText;


    public void UpdateScore(int playerNumber, int score)
    {
        switch (playerNumber)
        {
            case 0:
                playerScore1.text = $"Score: {score}";
                return;
            case 1:
                playerScore2.text = $"Score: {score}";
                return;
            case 2:
                playerScore3.text = $"Score: {score}";
                return;
            case 3:
                playerScore4.text = $"Score: {score}";
                return;
            default:
                return;
        }
    }

    public void StartWinScreen(int ID, string playerName)
    {
		switch (ID)
		{
			default:
            case 1:
                currentWinScreen = p1WinScreen;
                break;
            case 2:
                currentWinScreen = p2WinScreen;
                break;
            case 3:
                currentWinScreen = p3WinScreen;
                break;
            case 4:
                currentWinScreen = p4WinScreen;
                break;
		}

        currentWinScreen.SetActive(true);
        winGameText.text = $"{playerName} wins the game!";
    }

    public void StartWinRoundScreen(string playerName)
    {
        winRoundText.gameObject.SetActive(true);
        winRoundText.text = $"{playerName} wins the round!";
    }

    public void StartNewRound()
    {
        //currentWinScreen.SetActive(false);
        winRoundText.gameObject.SetActive(false);
    }

    public void StartNewGame()
    {
        currentWinScreen.SetActive(false);
        scoreScreen.SetActive(true);
        for (int i = 1; i <= 4; i++)
        {
            UpdateScore(i, 0);
        }
    }
}
