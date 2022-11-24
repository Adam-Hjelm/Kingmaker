using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PlayerUpgradeScript : MonoBehaviour
{
    public PlayerController playerStats;

    [Header("Upgrades")]


    [Space]

    public Button HealthButton;
    public Button DamageButton;
    public Button SpeedButton;
    public Button BulletSizeButton;

    public Button[] playerButtons;

    public Button cardButton;

    public int numberOfCards = 5;
    public Transform cardSpawnSpot;
    public bool doUpgrade;
    public GameObject card;

    void Start()
    {
        //for (int i = 0; i < numberOfCards; i++)
        //{
        //    //var UpgradeCardObj = Instantiate(card, cardSpawnSpot.position + new Vector3(4 * i, 0, 0), Quaternion.identity);
        //    //UpgradeCardObj.transform.parent = gameObject.transform;
        //}
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            return;
        }

        else if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text == "HEALTH")
        {
            for (int i = 0; i < playerButtons.Length; i++)
            {
                playerButtons[i].onClick.RemoveAllListeners();
                int closureIndex = i; // Prevents the closure problem
                playerButtons[i].onClick.AddListener(() => HealthUpgrade());
            }
        }

        else if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text == "DMG")
        {
            for (int i = 0; i < playerButtons.Length; i++)
            {
                playerButtons[i].onClick.RemoveAllListeners();
                int closureIndex = i; // Prevents the closure problem
                playerButtons[i].onClick.AddListener(() => DamageUpgrade());
            }
        }
    }

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
        Debug.Log("added health");
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
        Debug.Log("added damage");
        // Add Damage
    }
}
