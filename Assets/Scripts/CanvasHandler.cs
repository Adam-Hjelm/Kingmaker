using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        GameManager.Instance.AddCanvasHandler(this);
    }

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

    }
}
