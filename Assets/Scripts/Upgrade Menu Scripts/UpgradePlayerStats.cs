using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlayerStats : MonoBehaviour
{
    private UpgradeCardScript upgradeCardScript;
    private UpgradeController upgradeController;
    private PlayerController playerStats;
    private DisplayPlayerStats displayPlayerStats;

    // Start is called before the first frame update
    void Start()
    {
        upgradeController = GetComponent<UpgradeController>();
    }

    public void UpgradePlayer(int playerNumberToGiveStat)
    {
        playerStats = GameManager.Instance.GetPlayerInput(playerNumberToGiveStat).GetComponent<PlayerController>();
        upgradeCardScript = upgradeController.chosenUpgradeCard.GetComponent<UpgradeCardScript>();
        displayPlayerStats = upgradeController.displayPlayerStats;

        switch (upgradeCardScript.currentCardType)
        {
            case UpgradeCardScript.CardType.HealthUpButBiggerPlayer:
                IncreaseHealth(upgradeCardScript.maxHealthModifier);
                ChangePlayerScale(upgradeCardScript.sizeModifier);
                break;
            case UpgradeCardScript.CardType.DamageUpButEveryFourthBulletHeals:
                ChangeBulletDamage(upgradeCardScript.bulletDamageModifier);
                playerStats.healingBullets = true;
                playerStats.GetComponent<ShootController>().healingBulletsAmount++;
                displayPlayerStats.currentHealingBullets++;
                break;
            case UpgradeCardScript.CardType.SpeedUpButSlowerBulletSpeed:
                ChangePlayerSpeed(upgradeCardScript.moveSpeedModifier);
                ChangeBulletSpeed(-upgradeCardScript.moveSpeedModifier * 1.75f);
                break;
            case UpgradeCardScript.CardType.DamageUpButSlowerBulletSpeed:
                ChangeBulletDamage(upgradeCardScript.bulletDamageModifier);
                ChangeBulletSpeed(-upgradeCardScript.moveSpeedModifier * 1.75f);
                break;
            case UpgradeCardScript.CardType.FireRateUpButSmallerBullets:
                ChangeFireRate(upgradeCardScript.fireRateModifier);
                ChangeBulletScale(-upgradeCardScript.sizeModifier * 0.5f);
                break;
            case UpgradeCardScript.CardType.RandomSizedBullets:
                playerStats.bulletsRandomSized = true;
                displayPlayerStats.currentRandomSizedAmount++;
                playerStats.GetComponent<ShootController>().randomSizeBulletAmount++;
                break;
            case UpgradeCardScript.CardType.MoreBulletsButMoreSpread:
                playerStats.bulletAmount *= 2;
                playerStats.bulletSpread += 45;
                break;
        }
        //selectedUpgradeCard.GetComponent<SpriteRenderer>().enabled = false;
        upgradeController.upgradeCardButtons.Remove(upgradeController.chosenUpgradeCard);
    }

    private void ChangeFireRate(float fireRate)
    {
        playerStats.fireRate += fireRate;
        displayPlayerStats.firerateAnim.speed = playerStats.fireRate / 0.4f;
    }

    private void ChangePlayerSpeed(float moveSpeed)
    {
        playerStats.moveSpeed += moveSpeed;
        if (moveSpeed < 0)
        {
            displayPlayerStats.numOfSpeedBars--;
        }
        if (moveSpeed > 0)
        {
            displayPlayerStats.numOfSpeedBars++;
        }

    }

    private void ChangeBulletSpeed(float bulletSpeed = 4)
    {
        playerStats.GetComponent<ShootController>().bulletSpeed += bulletSpeed;
        displayPlayerStats.currentProjectileSpeedNerfs++;
    }

    private void ChangeBulletDamage(int bulletDamage = 1)
    {
        playerStats.bulletDamage += bulletDamage;
        if (bulletDamage < 0)
        {
            displayPlayerStats.numOfDamageBars--;
        }
        if (bulletDamage > 0)
        {
            displayPlayerStats.numOfDamageBars++;
        }
    }

    private void ChangeBulletScale(Vector3 sizeModifier)
    {
        playerStats.bulletSize += sizeModifier;
    }

    private void ChangePlayerScale(Vector3 sizeIncrease)
    {
        playerStats.gameObject.transform.localScale += sizeIncrease;
    }

    private void IncreaseHealth(int maxHealth)
    {
        playerStats.maxHealth += maxHealth;
        displayPlayerStats.numOfHealthBars++;
    }
}
