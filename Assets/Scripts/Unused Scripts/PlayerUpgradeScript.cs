using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PlayerUpgradeScript : MonoBehaviour
{
    public PlayerController playerStats;
    public UpgradeCardScript upgradeCardScript;

    [Header("Upgrades")]

    [Space]
    public int numberOfCards = 5;
    public Button cardButton;
    public Transform cardSpawnSpot;
    public GameObject lastSelectedGameObject;
    public Button selectedCard;
    public Button[] playerButtons;

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            selectedCard = null;
            return;
        }
        else if (EventSystem.current.currentSelectedGameObject.CompareTag("Card"))
        {
            selectedCard = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        }

        if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text == "HEALTH")
        {

            //for (int i = 0; i < playerButtons.Length; i++)
            //{
            //    playerButtons[i].onClick.RemoveAllListeners();
            //    playerButtons[i].onClick.AddListener(() => HealthUpgrade());
            //}
        }

        else if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text == "DMG")
        {
            selectedCard = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

            //for (int i = 0; i < playerButtons.Length; i++)
            //{
            //    playerButtons[i].onClick.RemoveAllListeners();
            //    playerButtons[i].onClick.AddListener(() => DamageUpgrade());
            //}
        }

        if (Input.GetButton("Cancel"))
        {
            ReturnFromPlayerButtons();
        }
    }

    private void ReturnFromPlayerButtons()
    {
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(selectedCard.gameObject);
    }

    public void ChangeToPlayerButtons()
    {
        lastSelectedGameObject = EventSystem.current.currentSelectedGameObject.GetComponent<GameObject>();

        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(playerButtons[0].gameObject);
    }

    #region Card Upgrades
    public void HealthUpgrade(/*int healthMod*/)
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Player1Button")
        {
            playerStats = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerController>();
        }

        if (EventSystem.current.currentSelectedGameObject.name == "Player2Button")
        {
            playerStats = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerController>();
        }
        if (EventSystem.current.currentSelectedGameObject.name == "Player3Button")
        {
            playerStats = GameObject.FindGameObjectWithTag("Player3").GetComponent<PlayerController>();
        }
        if (EventSystem.current.currentSelectedGameObject.name == "Player4Button")
        {
            playerStats = GameObject.FindGameObjectWithTag("Player4").GetComponent<PlayerController>();
        }
        playerStats.maxHealth += 100;
    }

    public void DamageUpgrade()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Player1Button")
        {
            playerStats = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerController>();
        }

        if (EventSystem.current.currentSelectedGameObject.name == "Player2Button")
        {
            playerStats = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerController>();
        }
        if (EventSystem.current.currentSelectedGameObject.name == "Player3Button")
        {
            playerStats = GameObject.FindGameObjectWithTag("Player3").GetComponent<PlayerController>();
        }
        if (EventSystem.current.currentSelectedGameObject.name == "Player4Button")
        {
            playerStats = GameObject.FindGameObjectWithTag("Player4").GetComponent<PlayerController>();
        }
        // Add Damage
    }


    #endregion
}
