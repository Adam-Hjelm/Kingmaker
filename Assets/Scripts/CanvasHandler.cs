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
    [SerializeField] GameObject winScreen;
    [SerializeField] TextMeshProUGUI winGameText;
    [SerializeField] TextMeshProUGUI winRoundText;


    public void UpdateScore(int playerNumber, int score)
    {
        switch (playerNumber)
        {
            case 1:
                playerScore1.text = $"Score: {score}";
                return;
            case 2:
                playerScore2.text = $"Score: {score}";
                return;
            case 3:
                playerScore3.text = $"Score: {score}";
                return;
            case 4:
                playerScore4.text = $"Score: {score}";
                return;
            default:
                return;
        }
    }

    public void StartWinScreen(string playerName)
    {
        winScreen.SetActive(true);
        winGameText.text = $"{playerName} wins the game!";
    }

    public void StartWinRoundScreen(string playerName)
    {
        winRoundText.gameObject.SetActive(true);
        winRoundText.text = $"{playerName} wins the round!";
    }

    public void StartNewRound()
    {
        winScreen.SetActive(false);
        winRoundText.gameObject.SetActive(false);
    }

    public void StartNewGame()
    {
        winScreen.SetActive(false);
        scoreScreen.SetActive(true);
        for (int i = 1; i <= 4; i++)
        {
            UpdateScore(i, 0);
        }
    }
}
