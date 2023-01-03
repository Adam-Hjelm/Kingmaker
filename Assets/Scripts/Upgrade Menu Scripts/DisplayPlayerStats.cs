using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerStats : MonoBehaviour
{
    //public int healthStatsAmount;
    public Sprite fullbar;
    public Sprite emptyBar;

    public int numOfHealthBars;
    public Image[] barsOfHealth;

    public int numOfDamageBars;
    public Image[] barsOfDamage;

    public int numOfSpeedBars;
    public Image[] barsOfSpeed;

    public Animator firerateAnim;

    private PlayerController playerStats;

    public int currentProjectileSpeedNerfs;
    public int maxProjectileSpeedModifiers;

    public int currentHealingBullets;
    public int maxHealingBullets;

    public bool hasRandomSizedBullets;

    [Header("Score Components")]
    public int playerNumberDisplayBelongsTo;
    public int currentScore;

    public Image scoreCrown1;
    public Image scoreCrown2;
    public Image scoreCrown3;

    public Sprite crownEmpty;
    public Sprite crownPoint;

    public void UpdateStatScreen(int playerNumberToUpdate)
    {
        // playerStats = GameManager.Instance.GetPlayerInput(playerNumberToUpdate).GetComponent<PlayerController>();

        for (int i = 0; i < barsOfHealth.Length; i++)
        {
            if (i < numOfHealthBars)
            {
                barsOfHealth[i].enabled = true;
            }
            else
            {
                barsOfHealth[i].enabled = false;
            }
        }
        for (int i = 0; i < barsOfDamage.Length; i++)
        {
            if (i < numOfDamageBars)
            {
                barsOfDamage[i].enabled = true;
            }
            else
            {
                barsOfDamage[i].enabled = false;
            }
        }
        for (int i = 0; i < barsOfSpeed.Length; i++)
        {
            if (i < numOfSpeedBars)
            {
                barsOfSpeed[i].enabled = true;
            }
            else
            {
                barsOfSpeed[i].enabled = false;
            }
        }
    }

    public void OnEnable()
    {
        if (GameManager.Instance.GetPlayerScore(playerNumberDisplayBelongsTo - 1) > 0 && GameManager.Instance.GetPlayerScore(playerNumberDisplayBelongsTo - 1) < 5)
        {
            currentScore = GameManager.Instance.GetPlayerScore(playerNumberDisplayBelongsTo - 1);
        }

        if (currentScore == 0)
        {
            scoreCrown1.sprite = crownEmpty;
            scoreCrown2.sprite = crownEmpty;
            scoreCrown3.sprite = crownEmpty;
        }
        else if (currentScore == 1)
        {
            scoreCrown1.sprite = crownPoint;
        }
        else if (currentScore == 2)
        {
            scoreCrown1.sprite = crownPoint;
            scoreCrown2.sprite = crownPoint;
        }

        if (GameManager.Instance.GetPlayerInput(playerNumberDisplayBelongsTo - 1) != null)
        {
            UpdateStatScreen(playerNumberDisplayBelongsTo);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public bool CheckIfStatMaxed(UpgradeCardScript.CardType cardType)
    {
        if (numOfHealthBars >= barsOfHealth.Length
            && (cardType == UpgradeCardScript.CardType.HealthUpButBiggerPlayer))
        {
            return true;
        }
        else if (numOfDamageBars >= barsOfDamage.Length
            && cardType == UpgradeCardScript.CardType.DamageUpButSlowerBulletSpeed)
        {
            return true;
        }
        else if (numOfSpeedBars >= barsOfSpeed.Length
            && cardType == UpgradeCardScript.CardType.SpeedUpButSlowerBulletSpeed)
        {
            return true;
        }
        else if (currentProjectileSpeedNerfs >= maxProjectileSpeedModifiers
            && (cardType == UpgradeCardScript.CardType.DamageUpButSlowerBulletSpeed ||
            cardType == UpgradeCardScript.CardType.SpeedUpButSlowerBulletSpeed))
        {
            return true;
        }
        else if (currentHealingBullets >= maxHealingBullets
            && cardType == UpgradeCardScript.CardType.FasterFireRateButEveryFourthBulletHeals)
        {
            return true;
        }
        else if (hasRandomSizedBullets == true
            && cardType == UpgradeCardScript.CardType.RandomSizedBullets)
        {
            return true;
        }
        return false;
    }

    public bool CheckIfStatMaxed(GameObject currentUpgradeCard) // TODO: Add feedback to show that the player has maxed said stat, change the max stat you can have to something higher
    {
        var currentUpgradeCardScript = currentUpgradeCard.GetComponent<UpgradeCardScript>();

        //switch (currentUpgradeCardScript.currentCardType)
        //{
        //    case UpgradeCardScript.CardType.HealthUpButBiggerPlayer:
        //        if (numOfHealthBars >= barsOfHealth.Length)
        //            return true;
        //        break;

        //    case UpgradeCardScript.CardType.FasterFireRateButEveryFourthBulletHeals:
        //        if (currentHealingBullets >= maxHealingBullets)
        //            return true;
        //        break;

        //    case UpgradeCardScript.CardType.SpeedUpButSlowerBulletSpeed:
        //        if (numOfSpeedBars >= barsOfSpeed.Length)
        //            return true;
        //        break;

        //    case UpgradeCardScript.CardType.DamageUpButSlowerBulletSpeed:
        //        if (currentHealingBullets >= maxHealingBullets)
        //            return true;
        //        break;

        //    case UpgradeCardScript.CardType.FireRateUpButSmallerBullets:
        //        if (currentHealingBullets >= maxHealingBullets)
        //            return true;
        //        break;

        //    case UpgradeCardScript.CardType.RandomSizedBullets:
        //        if (currentHealingBullets >= maxHealingBullets)
        //            return true;
        //        break;

        //    case UpgradeCardScript.CardType.MoreBulletsButMoreSpread:
        //        if (currentHealingBullets >= maxHealingBullets)
        //            return true;
        //        break;

        //    case UpgradeCardScript.CardType.AbilityCard:
        //        if (currentHealingBullets >= maxHealingBullets)
        //            return true;
        //        break;
        //}

        //return false;

        if (numOfHealthBars >= barsOfHealth.Length
            && (currentUpgradeCardScript.currentCardType == UpgradeCardScript.CardType.HealthUpButBiggerPlayer))
        {
            return true;
        }
        else if (numOfDamageBars >= barsOfDamage.Length
            && currentUpgradeCardScript.currentCardType == UpgradeCardScript.CardType.DamageUpButSlowerBulletSpeed)
        {
            return true;
        }
        else if (numOfSpeedBars >= barsOfSpeed.Length
            && currentUpgradeCardScript.currentCardType == UpgradeCardScript.CardType.SpeedUpButSlowerBulletSpeed)
        {
            return true;
        }
        else if (currentProjectileSpeedNerfs >= maxProjectileSpeedModifiers
            && (currentUpgradeCardScript.currentCardType == UpgradeCardScript.CardType.DamageUpButSlowerBulletSpeed ||
            currentUpgradeCardScript.currentCardType == UpgradeCardScript.CardType.SpeedUpButSlowerBulletSpeed))
        {
            return true;
        }
        else if (currentHealingBullets >= maxHealingBullets
            && currentUpgradeCardScript.currentCardType == UpgradeCardScript.CardType.FasterFireRateButEveryFourthBulletHeals)
        {
            return true;
        }
        else if(hasRandomSizedBullets == true)
        {
            return true;
        }
        return false;
    }
}
