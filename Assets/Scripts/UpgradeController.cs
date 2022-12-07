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

    //public Button[] upgradeCardButtons;
    public Button startUpgradeCard;
    public Button currentlySelectedCard;
    public Button chosenUpgradeCard;
    public GameObject cardButtonPrefab;
    public Canvas upgradeCanvas;
    public TextMeshProUGUI playerChooseText;

    private UpgradeCardScript upgradeCardScript;
    private PlayerController playerStats;

    public int playerNumberToGiveStat;
    public bool inPlayerButtons = false;

    public Vector3 cardOffset;
    public int playerToChooseCard = 2;

    public MultiplayerEventSystem playerEventSys1;
    public MultiplayerEventSystem playerEventSys2;
    public MultiplayerEventSystem playerEventSys3;
    public MultiplayerEventSystem playerEventSys4;

    public MultiplayerEventSystem eventSysInUse;

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
        }

        if (inPlayerButtons && chosenUpgradeCard != null)
        {
            chosenUpgradeCard.gameObject.transform.position = eventSysInUse.currentSelectedGameObject.GetComponent<Transform>().position + cardOffset;
        }
    }

    public void UpgradePlayerStat()
    {
        CheckPlayerToGiveStats();
        //playerStats = GameObject.FindGameObjectWithTag($"Player{playerNumberToGiveStat}").GetComponent<PlayerController>();
        //upgradeCardScript = selectedUpgradeCard.GetComponent<UpgradeCardScript>();

        //inPlayerButtons = false;

        //if (upgradeCardScript.currentCardType == UpgradeCardScript.CardType.HealthUp)
        //{
        //    playerStats.maxHealth += 100;
        //}

        //if (upgradeCardScript.currentCardType == UpgradeCardScript.CardType.DamageUp)
        //{
        //    playerStats.bulletDamage += 25;
        //}

        //if (upgradeCardScript.currentCardType == UpgradeCardScript.CardType.SpeedUp)
        //{
        //    playerStats.moveSpeed += 2;
        //}

        //if (upgradeCardScript.currentCardType == UpgradeCardScript.CardType.FireRateUp)
        //{
        //    playerStats.fireRate += 0.15f;
        //}
        ////selectedUpgradeCard.GetComponent<SpriteRenderer>().enabled = false;
        //upgradeCardButtons.Remove(selectedUpgradeCard);

        SpawnNewCards();

        inPlayerButtons = false;
    }

    private void CheckPlayerToGiveStats()
    {
        string playerButtonName = EventSystem.current.currentSelectedGameObject.GetComponent<Button>().name;

        if (playerButtonName.Contains("1"))
        {
            playerNumberToGiveStat = 1;
        }

        if (playerButtonName.Contains("2"))
        {
            playerNumberToGiveStat = 2;
        }

        if (playerButtonName.Contains("3"))
        {
            playerNumberToGiveStat = 3;
        }

        if (playerButtonName.Contains("4"))
        {
            playerNumberToGiveStat = 4;
        }
    }

    private void SpawnNewCards()
    {
        //Debug.Log("spawning next card..");

        for (int i = 0; i < upgradeCardButtons.Count; i++)
        {
            if (upgradeCardButtons[i] != null)
            {
                Destroy(upgradeCardButtons[i].gameObject);
            }
        }

        for (int i = 0; i < cardSpawnPos.Length; i++)
        {
            GameObject newButton = Instantiate(cardButtonPrefab, cardSpawnPos[i].position, Quaternion.identity, upgradeCanvas.transform);

            newButton.GetComponent<Button>().onClick.AddListener(MoveToPlayerButtons);
            startUpgradeCard = newButton.GetComponent<Button>();
            //newButton.transform.SetParent(upgradeCanvas.transform, true);

            upgradeCardButtons.Add(newButton.GetComponent<Button>());
        }

        CheckForNextPlayer();
    }

    private void CheckForNextPlayer()
    {
        playerToChooseCard++;

        playerChooseText.text = $"Player {playerToChooseCard},Choose a Card";

        if (playerToChooseCard == 1)
        {
            eventSysInUse = playerEventSys1;
        }

        else if (playerToChooseCard == 2)
        {
            eventSysInUse = playerEventSys2;
        }

        else if (playerToChooseCard == 3)
        {
            eventSysInUse = playerEventSys3;
        }

        else if (playerToChooseCard == 4)
        {
            eventSysInUse = playerEventSys4;
        }

        else if (playerToChooseCard > 4)
        {
            Invoke(nameof(FinishedUpgrade), 3);
        }

        playerEventSys1.SetSelectedGameObject(null);
        playerEventSys2.SetSelectedGameObject(null);
        playerEventSys3.SetSelectedGameObject(null);
        playerEventSys4.SetSelectedGameObject(null);

        eventSysInUse.SetSelectedGameObject(startUpgradeCard.gameObject);
    }

    void FinishedUpgrade()
    {
        GameManager.Instance.FinishedUpgrade();
    }

    public void MoveToPlayerButtons()
    {
        inPlayerButtons = true;
        upgradeCardButtons = upgradeCardButtons.Where(item => item != null).ToList();

        if (chosenUpgradeCard != null)
        {
            // Here you can make the card do some cool animations before it goes away and has been given to the player
            chosenUpgradeCard.gameObject.SetActive(false);
        }
        chosenUpgradeCard = currentlySelectedCard;

        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(playerButtons[0].gameObject);
    }
}
