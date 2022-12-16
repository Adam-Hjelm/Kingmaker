using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeCardScript : MonoBehaviour
{
    //private int amountOfStats = 7;
    private int amountOfRares = 4;
    private int randomNumber;

    public CardType currentCardType;

    [Space]

    //public RuntimeAnimatorController damageCardAnimController;
    //public RuntimeAnimatorController healthCardAnimController;
    //public RuntimeAnimatorController fireRateCardAnimController;
    //public RuntimeAnimatorController speedCardAnimController;

    public Sprite HealthUpButBiggerPlayerSprite;
    public Sprite HealthUpButSlowerPlayerSpeedSprite;
    public Sprite SpeedUpButSlowerBulletSpeedSprite;
    public Sprite DamageUpButSlowerBulletSpeedSprite;
    public Sprite FireRateUpButSmallerBulletsSprite;
    public Sprite BulletSpeedUpButSlowerFireRateSprite;
    public Sprite MoreBulletsButMoreSpreadSprite;



    //public Animator anim;

    [Header("Stats Upgrade")]
    public float moveSpeedModifier = 1.2f;
    public int maxHealthModifier = 4;
    public int bulletDamageModifier = 1;
    public float fireRateModifier = 0.3f;
    //public Vector3 sizeDecreaseModifier = new Vector3(0.6f, 0.6f, 0.6f);
    public Vector3 sizeModifier = new Vector3(0.6f, 0.6f, 0.6f);

    public enum CardType
    {
        HealthUpButBiggerPlayer,
        HealthUpButSlowerPlayerSpeed,
        SpeedUpButSlowerBulletSpeed,
        DamageUpButSlowerBulletSpeed,
        FireRateUpButSmallerBullets,
        BulletSpeedUpButSlowerFireRate,
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

    public void StatCard(int chosenStatCard)
    {
        //int chosenStatCard = Random.Range(0 + 1, amountOfStats + 1);

        //Debug.Log(chosenStatCard);

        switch (chosenStatCard)
        {
            case 1:
                Debug.Log("Given damage card");
                //anim.runtimeAnimatorController = damageCardAnimController;
                gameObject.GetComponent<Image>().sprite = DamageUpButSlowerBulletSpeedSprite;
                currentCardType = CardType.DamageUpButSlowerBulletSpeed;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "MORE DAMAGE \nBUT \nSLOWER FIREBALLS";
                break;

            case 2:
                Debug.Log("Given health card");
                //anim.runtimeAnimatorController = healthCardAnimController;
                gameObject.GetComponent<Image>().sprite = HealthUpButBiggerPlayerSprite;
                currentCardType = CardType.HealthUpButBiggerPlayer;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "MORE HEALTH\nBUT \nBIGGER SIZE";
                break;

            case 3:
                Debug.Log("Given health card");
                //anim.runtimeAnimatorController = healthCardAnimController;
                gameObject.GetComponent<Image>().sprite = HealthUpButSlowerPlayerSpeedSprite;
                currentCardType = CardType.HealthUpButSlowerPlayerSpeed;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "MORE HEALTH\nBUT \nSLOWER MOVESPEED";
                break;

            case 4:
                Debug.Log("Given fire rate card");
                //anim.runtimeAnimatorController = fireRateCardAnimController;
                gameObject.GetComponent<Image>().sprite = FireRateUpButSmallerBulletsSprite;
                currentCardType = CardType.FireRateUpButSmallerBullets;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "FASTER FIRE RATE\nBUT \nSMALLER FIREBALLS";
                break;

            case 5:
                Debug.Log("Given speed card");
                //anim.runtimeAnimatorController = speedCardAnimController;
                gameObject.GetComponent<Image>().sprite = SpeedUpButSlowerBulletSpeedSprite;
                currentCardType = CardType.SpeedUpButSlowerBulletSpeed;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "FASTER MOVESPEED\nBUT \nSLOWER FIREBALLS";
                break;
            case 6:
                Debug.Log("Given BulletSpeedUpButSlowerFireRate card");
                gameObject.GetComponent<Image>().sprite = BulletSpeedUpButSlowerFireRateSprite;
                currentCardType = CardType.BulletSpeedUpButSlowerFireRate;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "FASTER FIREBALLS\nBUT \nSLOWER FIRE RATE";
                break;
            case 7:
                Debug.Log("Given more bullets card card");
                gameObject.GetComponent<Image>().sprite = MoreBulletsButMoreSpreadSprite;
                currentCardType = CardType.MoreBulletsButMoreSpread;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "MORE FIREBALLS\nBUT \nMORE SPREAD";
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
