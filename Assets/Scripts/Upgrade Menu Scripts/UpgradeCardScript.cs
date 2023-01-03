using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeCardScript : MonoBehaviour
{
    //private int amountOfStats = 7;
    //private int amountOfRares = 4;
    private int randomNumber;

    public CardType currentCardType;

    [Space]

    //public RuntimeAnimatorController damageCardAnimController;
    //public RuntimeAnimatorController healthCardAnimController;
    //public RuntimeAnimatorController fireRateCardAnimController;
    //public RuntimeAnimatorController speedCardAnimController;

    public Sprite HealthUpButBiggerPlayerSprite;
    public Sprite EveryFourthBulletHealsSprite;
    public Sprite SpeedUpButSlowerBulletSpeedSprite;
    public Sprite DamageUpButSlowerBulletSpeedSprite;
    public Sprite FireRateUpButSmallerBulletsSprite;
    public Sprite randomSizedBullets;
    public Sprite MoreBulletsButMoreSpreadSprite;



    //public Animator anim;

    [Header("Stats Upgrade")]
    public float moveSpeedModifier = 1.2f;
    public int maxHealthModifier = 4;
    public int bulletDamageModifier = 1;
    public float fireRateModifier = 0.3f;
    //public Vector3 sizeDecreaseModifier = new Vector3(0.6f, 0.6f, 0.6f);
    public Vector3 sizeModifier = new Vector3(0.5f, 0.5f, 0.5f);

    public enum CardType
    {
        HealthUpButBiggerPlayer,
        FasterFireRateButEveryFourthBulletHeals,
        SpeedUpButSlowerBulletSpeed,
        DamageUpButSlowerBulletSpeed,
        FireRateUpButSmallerBullets,
        RandomSizedBullets,
        MoreBulletsButMoreSpread,
        AbilityCard,
    }

    void Start()
    {
        //anim = gameObject.GetComponent<Animator>();

        randomNumber = Random.Range(0, 100);
        if (randomNumber <= 100)
        {

        }


        //if (randomNumber > 80) TO BE IMPLEMENTED AT A LATER DATE
        //{
        //    SpecialCard();
        //    //this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "SPECIAL\n CARD";
        //}
    }

    public void StatCard(CardType chosenStatCard)
    {
        //int chosenStatCard = Random.Range(0 + 1, amountOfStats + 1);

        switch (chosenStatCard)
        {
            case CardType.HealthUpButBiggerPlayer:
                //anim.runtimeAnimatorController = healthCardAnimController;
                gameObject.GetComponent<Image>().sprite = HealthUpButBiggerPlayerSprite;
                currentCardType = CardType.HealthUpButBiggerPlayer;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "+HEALTH\n---- \n+PLAYER SIZE";
                break;

            case CardType.FasterFireRateButEveryFourthBulletHeals:
                //anim.runtimeAnimatorController = healthCardAnimController;
                gameObject.GetComponent<Image>().sprite = EveryFourthBulletHealsSprite;
                currentCardType = CardType.FasterFireRateButEveryFourthBulletHeals;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "+FIRE RATE\n---- \n EVERY FOURTH SHOT HEALS";
                break;

            case CardType.SpeedUpButSlowerBulletSpeed:
                //anim.runtimeAnimatorController = speedCardAnimController;
                gameObject.GetComponent<Image>().sprite = SpeedUpButSlowerBulletSpeedSprite;
                currentCardType = CardType.SpeedUpButSlowerBulletSpeed;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "+MOVESPEED\n---- \n-PROJECTILE SPEED";
                break;

            case CardType.DamageUpButSlowerBulletSpeed:
                //anim.runtimeAnimatorController = damageCardAnimController;
                gameObject.GetComponent<Image>().sprite = DamageUpButSlowerBulletSpeedSprite;
                currentCardType = CardType.DamageUpButSlowerBulletSpeed;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "+DAMAGE \n---- \n-PROJECTILE SPEED";
                break;

            case CardType.FireRateUpButSmallerBullets:
                //anim.runtimeAnimatorController = fireRateCardAnimController;
                gameObject.GetComponent<Image>().sprite = FireRateUpButSmallerBulletsSprite;
                currentCardType = CardType.FireRateUpButSmallerBullets;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "+FIRE RATE\n---- \n-PROJECTILE SIZE";
                break;

            case CardType.RandomSizedBullets:
                gameObject.GetComponent<Image>().sprite = randomSizedBullets;
                currentCardType = CardType.RandomSizedBullets;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "SIZE OF PROJECTILES ARE RANDOMIZED EVERY SHOT";
                break;

            case CardType.MoreBulletsButMoreSpread:
                gameObject.GetComponent<Image>().sprite = MoreBulletsButMoreSpreadSprite;
                currentCardType = CardType.MoreBulletsButMoreSpread;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "+PROJECTILE AMOUNT\n--- \n+PROJECTILE SPREAD";
                break;
        }
    }

    //private void SpecialCard()
    //{
    //    Debug.Log("Given SPECIAL CARD");
    //    currentCardType = CardType.AbilityCard;
    //    int chosenRare = Random.Range(0, amountOfRares);
    //    switch (chosenRare)
    //    {
    //        case 1:

    //            break;

    //        case 2:

    //            break;

    //        case 3:

    //            break;

    //        case 4:

    //            break;
    //    }
    //}
}
