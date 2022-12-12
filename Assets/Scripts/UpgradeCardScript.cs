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

    [Space]

    public RuntimeAnimatorController damageCardAnimController;
    public RuntimeAnimatorController healthCardAnimController;
    public RuntimeAnimatorController fireRateCardAnimController;
    public RuntimeAnimatorController speedCardAnimController;



    public Animator anim;

    [Header("Stats Upgrade")]
    public float moveSpeedModifier = 2f;
    public int maxHealthModifier = 1;
    public int bulletDamageModifier = 1;
    public float fireRateModifier = 0.2f;
    public float sizeDecreaseModifier = 0.5f;
    public float sizeIncreaseModifier = 1.5f;

    public enum CardType
    {
        DamageUpButSlowerBulletSpeed,
        HealthUpButBiggerPlayer,
        FireRateUpButSmallerBullets,
        SpeedUp,
        AbilityCard,
    }

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        randomNumber = Random.Range(0, 100);
        if (randomNumber <= 100)
        {
            StatCard();
        }

        //if (randomNumber > 80) TO BE IMPLEMENTED AT A LATER DATE
        //{
        //    SpecialCard();
        //    //this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "SPECIAL\n CARD";
        //}
    }

    private void StatCard()
    {
        int chosenStat = Random.Range(0, amountOfStats + 1);

        switch (chosenStat)
        {
            case 1:
                Debug.Log("Given damage card");
                //this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "DMG";
                anim.runtimeAnimatorController = damageCardAnimController;
                currentCardType = CardType.DamageUpButSlowerBulletSpeed;
                break;

            case 2:
                Debug.Log("Given health card");
                //this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "HEALTH";
                anim.runtimeAnimatorController = healthCardAnimController;
                currentCardType = CardType.HealthUpButBiggerPlayer;
                break;

            case 3:
                Debug.Log("Given fire rate card");
                //this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "FIRE RATE";
                anim.runtimeAnimatorController = fireRateCardAnimController;
                currentCardType = CardType.FireRateUpButSmallerBullets;
                break;

            case 4:
                Debug.Log("Given speed card");
                //this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "SPEED";
                anim.runtimeAnimatorController = speedCardAnimController;
                currentCardType = CardType.SpeedUp;
                break;
        }

        if (currentCardType == CardType.DamageUpButSlowerBulletSpeed)
        {
            anim.runtimeAnimatorController = damageCardAnimController;
        }
    }

    private void SpecialCard()
    {
        Debug.Log("Given SPECIAL CARD");
        currentCardType = CardType.AbilityCard;
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
