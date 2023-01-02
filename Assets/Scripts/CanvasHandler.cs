using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour
{
    //public string RoundText { get => roundText.text; set => roundText.text = value; }
    //[SerializeField] TextMeshProUGUI roundText;
    //[SerializeField] TextMeshProUGUI playerScore1;
    //[SerializeField] TextMeshProUGUI playerScore2;
    //[SerializeField] TextMeshProUGUI playerScore3;
    //[SerializeField] TextMeshProUGUI playerScore4;

    [SerializeField] GameObject scoreScreen;
    [SerializeField] WinningScreen winningScreen;
    [SerializeField] TextMeshProUGUI winGameText;
    [SerializeField] TextMeshProUGUI winRoundText;


    //public void UpdateScore(int playerNumber, int score)
    //{
    //    switch (playerNumber)
    //    {
    //        case 0:
    //            playerScore1.text = $"Score: {score}";
    //            return;
    //        case 1:
    //            playerScore2.text = $"Score: {score}";
    //            return;
    //        case 2:
    //            playerScore3.text = $"Score: {score}";
    //            return;
    //        case 3:
    //            playerScore4.text = $"Score: {score}";
    //            return;
    //        default:
    //            return;
    //    }
    //}

    public void StartWinScreen(int ID, string playerName, GameObject playerObject, Sprite playerSprite)
    {
        winningScreen.characterSprite.sprite = playerSprite;
        winningScreen.gameObject.SetActive(true);
        winGameText.text = $"{playerName} wins the game!";
        StartCoroutine(DelayWinMenuInteraction(playerObject));
    }

    private IEnumerator DelayWinMenuInteraction(GameObject playerObject)
    {
        yield return new WaitForSeconds(3);
        playerObject.GetComponentInChildren<MultiplayerEventSystem>().SetSelectedGameObject(winningScreen.GetComponentInChildren<Button>().gameObject);
    }

    public void StartWinRoundScreen(string playerName)
    {
        winRoundText.gameObject.SetActive(true);
        winRoundText.text = $"{playerName} wins the round!";
    }

    public void DisableWinRoundText()
    {
        winRoundText.gameObject.SetActive(false);
    }

    public void StartNewGame()
    {
        winningScreen.gameObject.SetActive(false);
        scoreScreen.SetActive(true);
        //for (int i = 1; i <= 4; i++)
        //{
        //    UpdateScore(i, 0);
        //}
    }
}
