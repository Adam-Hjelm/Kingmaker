using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeCardScript : MonoBehaviour
{
    private int amountOfStats = 6;
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
    public float moveSpeedModifier = 1.25f;
    public int maxHealthModifier = 1;
    public int bulletDamageModifier = 1;
    public float fireRateModifier = 0.2f;
    public float sizeDecreaseModifier = 0.75f;
    public float sizeIncreaseModifier = 1.25f;

    public enum CardType
    {
        HealthUpButBiggerPlayer,
        HealthUpButSlowerPlayerSpeed,
        SpeedUp,
        DamageUpButSlowerBulletSpeed,
        FireRateUpButSmallerBullets,
        BulletSpeedUpButSlowerFireRate,
        MoreBulletsButMoreSpread,
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
        int chosenStatCard = Random.Range(0, amountOfStats + 1);

        switch (chosenStatCard)
        {
            case 1:
                Debug.Log("Given damage card");
                //this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "DMG";
                anim.runtimeAnimatorController = damageCardAnimController;
                currentCardType = CardType.DamageUpButSlowerBulletSpeed;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentCardType.ToString();
                break;

            case 2:
                Debug.Log("Given health card");
                //this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "HEALTH";
                anim.runtimeAnimatorController = healthCardAnimController;
                currentCardType = CardType.HealthUpButBiggerPlayer;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentCardType.ToString();
                break;

            case 3:
                Debug.Log("Given fire rate card");
                //this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "FIRE RATE";
                anim.runtimeAnimatorController = fireRateCardAnimController;
                currentCardType = CardType.FireRateUpButSmallerBullets;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentCardType.ToString();
                break;

            case 4:
                Debug.Log("Given speed card");
                //this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "SPEED";
                anim.runtimeAnimatorController = speedCardAnimController;
                currentCardType = CardType.SpeedUp;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentCardType.ToString();
                break;
            case 5:
                Debug.Log("Given speed card");
                //this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "SPEED";
                //anim.runtimeAnimatorController = speedCardAnimController;
                currentCardType = CardType.BulletSpeedUpButSlowerFireRate;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentCardType.ToString();
                break;
            case 6:
                Debug.Log("Given speed card");
                //this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "SPEED";
                //anim.runtimeAnimatorController = speedCardAnimController;
                currentCardType = CardType.MoreBulletsButMoreSpread;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentCardType.ToString();
                break;
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
