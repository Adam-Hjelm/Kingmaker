using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PlayerUpgradeScript : MonoBehaviour
{
    public PlayerHealth playerHP;

    [Header("Upgrades")]


    [Space]

    public Button cardButton;

    public int numberOfCards = 5;
    public Transform cardSpawnSpot;
    public bool doUpgrade;
    public GameObject card;


    private void Awake()
    {
        //cardButton.onClick.AddListener(HealthUpgrade);
    }

    void cardButtonOnClick()
    {
        Debug.Log("test");
    }

    void Start()
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            //var UpgradeCardObj = Instantiate(card, cardSpawnSpot.position + new Vector3(4 * i, 0, 0), Quaternion.identity);
            //UpgradeCardObj.transform.parent = gameObject.transform;
        }
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            Debug.Log("CHOSE nothing");
            return;
        }


        if (EventSystem.current.currentSelectedGameObject.name.Contains("Player"))
        {
            cardButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>(); // BAD POLLING THING, FIX LATER
            Debug.Log("CHOSE player");
        }

        else if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text == "HEALTH")
        {
            //cardButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            Debug.Log("CHOSE HEALTH");
            cardButton.onClick.AddListener(HealthUpgrade);
        }

        else if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text == "DMG")
        {
            //cardButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

            cardButton.onClick.AddListener(DamageUpgrade);
        }


    }

    public void HealthUpgrade(/*int healthMod*/)
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Player1Button")
        {
            playerHP = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerHealth>();
        }

        if (EventSystem.current.currentSelectedGameObject.name == "Player2Button")
        {
            playerHP = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerHealth>();
        }
        if (EventSystem.current.currentSelectedGameObject.name == "Player3Button")
        {
            playerHP = GameObject.FindGameObjectWithTag("Player3").GetComponent<PlayerHealth>();
        }
        if (EventSystem.current.currentSelectedGameObject.name == "Player4Button")
        {
            playerHP = GameObject.FindGameObjectWithTag("Player4").GetComponent<PlayerHealth>();
        }

        playerHP.addedHealth += 100;
    }

    public void DamageUpgrade()
    {
        Debug.Log("Damage Up!");
    }
}
