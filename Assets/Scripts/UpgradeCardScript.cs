using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeCardScript : MonoBehaviour
{
    private int amountOfStats = 4;
    private int amountOfRares = 4;
    private int randomNumber;

    public CardType currentCardType;

    [Header("Stats Upgrade")]
    public float moveSpeedModifier = 2f;
    public int maxHealthModifier = 100;
    public int bulletDamageModifier = 25;
    public float fireRateModifier = 0.2f;

    public enum CardType
    {
        DamageUp,
        HealthUp,
        FireRateUp,
        SpeedUp,
        SpecialCard,
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
            SpecialCard();
            //this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "SPECIAL\n CARD";
        }
    }

    private void StatCard()
    {
        int chosenStat = Random.Range(0, amountOfStats);

        switch (chosenStat)
        {
            case 1:
                Debug.Log("Given damage card");
                //this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "DMG";
                currentCardType = CardType.DamageUp;
                break;

            case 2:
                Debug.Log("Given health card");
                //this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "HEALTH";
                currentCardType = CardType.HealthUp;
                break;

            case 3:
                Debug.Log("Given fire rate card");
                //this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "FIRE RATE";
                currentCardType = CardType.FireRateUp;
                break;

            case 4:
                Debug.Log("Given speed card");
                //this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "SPEED";
                currentCardType = CardType.SpeedUp;
                break;
        }
    }

    private void SpecialCard()
    {
        Debug.Log("Given SPECIAL CARD");
        currentCardType = CardType.SpecialCard;
        int chosenRare = Random.Range(0, amountOfRares);
        switch (chosenRare)
        {
            case 1:

                break;

            case 2:

                break;

            case 3:

                break;

            case 4:

                break;
        }
    }
}
