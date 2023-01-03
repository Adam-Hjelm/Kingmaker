using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour
{
    [SerializeField] WinningScreen winningScreen;
    [SerializeField] TextMeshProUGUI winGameText;
    [SerializeField] TextMeshProUGUI winRoundText;


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
    }
}
