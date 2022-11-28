using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeCardScript : MonoBehaviour
{
    private int amountOfStats = 4;
    private int randomNumber;

    public PlayerUpgradeScript playerUpgradeScript;
    public CardType currentCardType;

    public enum CardType
    {
        DamageUp,
        HealthUp,
        BulletSize,
        SpeedUp,
    }

    void Start()
    {
        randomNumber = Random.Range(0, 100);
        if (randomNumber <= 80)
        {
            StatCard();
        }

        if (randomNumber > 80)
        {
            //Insert special card function
            this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "SPECIAL\n CARD";
        }
    }

    private void StatCard()
    {
        int chosenStat = Random.Range(0, amountOfStats);

        switch (chosenStat)
        {
            case 1:
                Debug.Log("Given damage card");
                this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "DMG";
                currentCardType = CardType.DamageUp;
                break;

            case 2:
                Debug.Log("Given health card");
                this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "HEALTH";
                currentCardType = CardType.HealthUp;
                break;

            case 3:
                Debug.Log("Given bulletSize card");
                this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "BULLET\n SIZE";
                currentCardType = CardType.BulletSize;
                break;

            case 4:
                Debug.Log("Given speed card");
                this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "SPEED";
                currentCardType = CardType.SpeedUp;
                break;
        }
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            if (currentCardType == CardType.HealthUp)       // TODO: Make this code more clean by moving it into a single for loop?
            {
                for (int i = 0; i < playerUpgradeScript.playerButtons.Length; i++)
                {
                    playerUpgradeScript.playerButtons[i].onClick.RemoveAllListeners();
                    playerUpgradeScript.playerButtons[i].onClick.AddListener(() => playerUpgradeScript.HealthUpgrade());
                }
            }

            if (currentCardType == CardType.DamageUp)
            {
                for (int i = 0; i < playerUpgradeScript.playerButtons.Length; i++)
                {
                    playerUpgradeScript.playerButtons[i].onClick.RemoveAllListeners();
                    playerUpgradeScript.playerButtons[i].onClick.AddListener(() => playerUpgradeScript.DamageUpgrade());
                }
            }

            if (currentCardType == CardType.BulletSize)
            {

            }

            if (currentCardType == CardType.SpeedUp)
            {

            }
        }
    }

}
