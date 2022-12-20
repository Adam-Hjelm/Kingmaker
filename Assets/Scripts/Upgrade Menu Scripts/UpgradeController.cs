using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class UpgradeController : MonoBehaviour
{
    public Transform[] cardSpawnPos;
    public Button[] playerButtons;
    public List<Button> upgradeCardButtons = new List<Button>();
    private int amountOfTimesSpawned = 0;

    //public Button[] upgradeCardButtons;
    public Button startUpgradeCard;
    public Button currentlySelectedCard;
    public Button chosenUpgradeCard;
    public GameObject cardButtonPrefab;
    public Canvas upgradeCanvas;
    public TextMeshProUGUI playerChooseText;
    public GameObject textPickCard;
    public GameObject textPickCard2;
    public GameObject textChoosePlayer;
    public GameObject arrowIndicator;
    public Animation textfade;
    public Animator cardAnim;

    private UpgradePlayerStats upgradePlayerStats;
    public DisplayPlayerStats displayPlayerStats;

    public GameObject upgradeCardsHolder;
    public int playerNumberToGiveStat;
    public bool inPlayerButtons = false;

    public Vector3 cardOffset;
    public int playerToChooseCard = 1;

    public Color startColor;
    public Color grayedOutColor;

    public MultiplayerEventSystem playerEventSys1;
    public MultiplayerEventSystem playerEventSys2;
    public MultiplayerEventSystem playerEventSys3;
    public MultiplayerEventSystem playerEventSys4;

    public MultiplayerEventSystem eventSysInUse;


    public void OnEnable()
    { // kanske måste göra en null check ifall vi någonsin ska ha det att funka med mindre än 4 spelare

        eventSysInUse = playerEventSys1;
        eventSysInUse.SetSelectedGameObject(startUpgradeCard.gameObject);
    }

    void Start()
    {
        upgradePlayerStats = GetComponent<UpgradePlayerStats>();
        SpawnNewCards();
    }

    void Update()
    {
        if (eventSysInUse.currentSelectedGameObject == null)
        {
            currentlySelectedCard = null;
            return;
        }
        else if (eventSysInUse.currentSelectedGameObject.CompareTag("Card"))
        {
            currentlySelectedCard = eventSysInUse.currentSelectedGameObject.GetComponent<Button>();
            arrowIndicator.transform.position = eventSysInUse.currentSelectedGameObject.GetComponent<Transform>().position + cardOffset;
        }

        if (inPlayerButtons && chosenUpgradeCard != null)
        {
            arrowIndicator.transform.position = eventSysInUse.currentSelectedGameObject.GetComponent<Transform>().position + cardOffset;
            //chosenUpgradeCard.gameObject.transform.position = eventSysInUse.currentSelectedGameObject.GetComponent<Transform>().position + cardOffset;
        }
    }

    public void UpgradePlayerStat()
    {
        CheckPlayerToGiveStats();

        cardAnim.SetTrigger("MoveCardBack");
        inPlayerButtons = true;

        if (GameManager.Instance.GetPlayerInput(playerNumberToGiveStat) == null)
        {
            Debug.Log((playerNumberToGiveStat + 1) + ", No Player Detected");
            return;
        }
        else
        {
            upgradePlayerStats.UpgradePlayer(playerNumberToGiveStat);
            displayPlayerStats.UpdateStatScreen(playerNumberToGiveStat);

            SpawnNewCards();

            inPlayerButtons = false;
            textPickCard.SetActive(false);
            textPickCard2.SetActive(true);
            textChoosePlayer.SetActive(false);
        }
    }


    private void CheckPlayerToGiveStats()
    {
        string playerButtonName = eventSysInUse.currentSelectedGameObject.GetComponent<Button>().name;
        displayPlayerStats = eventSysInUse.currentSelectedGameObject.GetComponentInChildren<DisplayPlayerStats>();
        if (playerButtonName.Contains("1"))
        {
            playerNumberToGiveStat = 0;

        }
        else if (playerButtonName.Contains("2"))
        {
            playerNumberToGiveStat = 1;
        }
        else if (playerButtonName.Contains("3"))
        {
            playerNumberToGiveStat = 2;
        }
        else if (playerButtonName.Contains("4"))
        {
            playerNumberToGiveStat = 3;
        }
    }

    private void SpawnNewCards()
    {

        amountOfTimesSpawned++;
        //chosenUpgradeCard.gameObject.SetActive(false);
        //chosenUpgradeCard = null;

        for (int i = 0; i < upgradeCardButtons.Count; i++)
        {
            if (upgradeCardButtons[i] != null)
            {
                Destroy(upgradeCardButtons[i].gameObject);
            }
        }

        upgradeCardButtons = upgradeCardButtons.Where(item => item != null).ToList();

        List<int> randomNumbers = new List<int>();

        for (int i = 0; i < cardSpawnPos.Length; i++)
        {
            int randomNumber = UnityEngine.Random.Range(1, 8);
            int currentLoopNumber = 0;
            while (randomNumbers.Any(r => r == randomNumber) && currentLoopNumber < 10000)
            {
                randomNumber = UnityEngine.Random.Range(1, 8);
                currentLoopNumber++;
            }
            randomNumbers.Add(randomNumber);

            var newButton = Instantiate(cardButtonPrefab, cardSpawnPos[i].position, Quaternion.identity, upgradeCardsHolder.transform).GetComponent<Button>();
            newButton.GetComponent<UpgradeCardScript>().StatCard(randomNumber);
            newButton.onClick.AddListener(MoveToPlayerButtons);
            //newButton.transform.SetParent(upgradeCanvas.transform, true);
            startUpgradeCard = newButton;

            upgradeCardButtons.Add(newButton.GetComponent<Button>());
        }
        if (chosenUpgradeCard != null)
        {
            chosenUpgradeCard.gameObject.SetActive(false);
        }
        //chosenUpgradeCard = null;
        ColorCoordinateText(playerToChooseCard);
        //startUpgradeCard = upgradeCardButtons[0].GetComponent<Button>();

        if (amountOfTimesSpawned <= 1)
        {
            eventSysInUse = playerEventSys1;
            eventSysInUse.SetSelectedGameObject(startUpgradeCard.gameObject);
            return;
        }
        else
        {
            CheckForNextPlayer();
        }
    }

    private void ColorCoordinateText(int playerToGiveColorOf)
    {
        switch (playerToGiveColorOf)
        {
            case 1:
                textPickCard.GetComponent<TextMeshProUGUI>().text = $"<color=#ff372e>PLAYER {playerToGiveColorOf}</color>,CHOOSE A CARD";
                textPickCard2.GetComponent<TextMeshProUGUI>().text = $"<color=#ff372e>PLAYER {playerToGiveColorOf}</color>,CHOOSE A CARD";
                textChoosePlayer.GetComponent<TextMeshProUGUI>().text = $"<color=#ff372e>PLAYER {playerToGiveColorOf}</color>,CHOOSE A PLAYER";
                break;
            case 2:
                textPickCard.GetComponent<TextMeshProUGUI>().text = $"<color=#4498ff>PLAYER {playerToGiveColorOf}</color>,CHOOSE A CARD";
                textPickCard2.GetComponent<TextMeshProUGUI>().text = $"<color=#4498ff>PLAYER {playerToGiveColorOf}</color>,CHOOSE A CARD";
                textChoosePlayer.GetComponent<TextMeshProUGUI>().text = $"<color=#4498ff>PLAYER {playerToGiveColorOf}</color>,CHOOSE A PLAYER";
                break;
            case 3:
                textPickCard.GetComponent<TextMeshProUGUI>().text = $"<color=#96e431>PLAYER {playerToGiveColorOf}</color>,CHOOSE A CARD";
                textPickCard2.GetComponent<TextMeshProUGUI>().text = $"<color=#96e431>PLAYER {playerToGiveColorOf}</color>,CHOOSE A CARD";
                textChoosePlayer.GetComponent<TextMeshProUGUI>().text = $"<color=#96e431>PLAYER {playerToGiveColorOf}</color>,CHOOSE A PLAYER";
                break;
            case 4:
                textPickCard.GetComponent<TextMeshProUGUI>().text = $"<color=#ec3c9a>PLAYER {playerToGiveColorOf}</color>,CHOOSE A CARD";
                textPickCard2.GetComponent<TextMeshProUGUI>().text = $"<color=#ec3c9a>PLAYER {playerToGiveColorOf}</color>,CHOOSE A CARD";
                textChoosePlayer.GetComponent<TextMeshProUGUI>().text = $"<color=#ec3c9a>PLAYER {playerToGiveColorOf}</color>,CHOOSE A PLAYER";
                break;
        }
    }

    private void CheckForNextPlayer()
    {
        for (int i = 0; i < playerButtons.Length; i++)
        {
            playerButtons[i].GetComponent<Image>().color = Color.white;

        }

        switch (playerToChooseCard)
        {
            case 1:
                eventSysInUse = playerEventSys2;
                playerToChooseCard = 2;
                break;
            case 2:
                if (playerEventSys3 == null)
                {
                    playerEventSys1.SetSelectedGameObject(null);
                    playerEventSys2.SetSelectedGameObject(null);

                    Invoke(nameof(FinishedUpgrade), 0.1f);
                    return;
                }
                else
                {
                    eventSysInUse = playerEventSys3;
                    playerToChooseCard = 3;
                }
                break;
            case 3:
                if (playerEventSys4 == null)
                {
                    playerEventSys1.SetSelectedGameObject(null);
                    playerEventSys2.SetSelectedGameObject(null);
                    playerEventSys3.SetSelectedGameObject(null);

                    Invoke(nameof(FinishedUpgrade), 0.1f);
                    return;
                }
                else
                {
                    eventSysInUse = playerEventSys4;
                    playerToChooseCard = 4;
                }
                break;
            case 4:
                //eventSysInUse = playerEventSys1;
                //playerToChooseCard = 1;

                playerEventSys1.SetSelectedGameObject(null);
                playerEventSys2.SetSelectedGameObject(null);
                if (playerEventSys3 != null)
                {
                    playerEventSys3.SetSelectedGameObject(null);
                }
                if (playerEventSys4 != null)
                {
                    playerEventSys4.SetSelectedGameObject(null);
                }
                Invoke(nameof(FinishedUpgrade), 0.1f);

                return;
                //break;
        }

        ColorCoordinateText(playerToChooseCard);

        playerEventSys1.SetSelectedGameObject(null);
        playerEventSys2.SetSelectedGameObject(null);
        if (playerEventSys3 != null)
        {
            playerEventSys3.SetSelectedGameObject(null);
        }
        if (playerEventSys4 != null)
        {
            playerEventSys4.SetSelectedGameObject(null);
        }

        //if (playerToChooseCard >= 4)
        //    return;

        eventSysInUse.SetSelectedGameObject(startUpgradeCard.gameObject);
    }

    void FinishedUpgrade()
    {
        cardAnim.SetTrigger("MoveCardBack");

        for (int i = 0; i < playerButtons.Length; i++)
        {
            //displayPlayerStats = playerButtons[i].GetComponentInChildren<DisplayPlayerStats>();
            //displayPlayerStats.CleanupStatScreen(i);
        }
        eventSysInUse = playerEventSys1;
        playerToChooseCard = 1;

        ColorCoordinateText(playerToChooseCard);

        amountOfTimesSpawned = 1;
        GameManager.Instance.FinishedUpgrade();
    }

    public void MoveToPlayerButtons() // TODO: GE EN NONO SOUND EFFECT NÄR SPELAREN FÖRSÖKER VÄLJA SIG SJÄLV ATT GE KORT
    {
        for (int i = 0; i < playerButtons.Length; i++)
        {
            playerButtons[i].onClick.RemoveAllListeners();
            playerButtons[i].onClick.AddListener(UpgradePlayerStat);
            playerButtons[i].GetComponent<Image>().color = Color.white;
        }

        if (GameManager.Instance.GetPlayerInput(2) == null)
        {
            playerButtons[2].onClick.RemoveListener(UpgradePlayerStat);
            playerButtons[2].GetComponent<Image>().color = grayedOutColor;
        }
        if (GameManager.Instance.GetPlayerInput(3) == null)
        {
            playerButtons[3].onClick.RemoveListener(UpgradePlayerStat);
            playerButtons[3].GetComponent<Image>().color = grayedOutColor;
        }

        playerButtons[playerToChooseCard - 1].onClick.RemoveListener(UpgradePlayerStat); // H�r kan vi graya ut knappen s� att spelaren inte tror att den kan interagera med sig sj�lv
        playerButtons[playerToChooseCard - 1].GetComponent<Image>().color = grayedOutColor;

        inPlayerButtons = true;
        cardAnim.SetTrigger("MoveCard");
        textPickCard.SetActive(true);
        textPickCard2.SetActive(false);
        textChoosePlayer.SetActive(true);
        //textChoosePlayer.GetComponent<TextMeshProUGUI>().text = $"PLAYER {playerToChooseCard}, CHOOSE A PLAYER";
        //if (chosenUpgradeCard != null)
        //{
        //    // Here you can make the card do some cool animations before it goes away and has been given to the player
        //    chosenUpgradeCard.gameObject.SetActive(false);
        //}
        chosenUpgradeCard = currentlySelectedCard;

        eventSysInUse.GetComponent<EventSystem>().SetSelectedGameObject(playerButtons[0].gameObject);
    }
}
