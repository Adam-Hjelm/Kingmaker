using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlayerStats : MonoBehaviour
{
    private UpgradeCardScript upgradeCardScript;
    private UpgradeController upgradeController;
    private PlayerController playerStats;

    // Start is called before the first frame update
    void Start()
    {
        upgradeController = GetComponent<UpgradeController>();
    }

    public void UpgradePlayer(int playerNumberToGiveStat)
    {
        Debug.Log("Upgrade player stat");

        //playerStats = GameObject.FindGameObjectWithTag($"Player{playerNumberToGiveStat}").GetComponent<PlayerController>();
        playerStats = GameManager.Instance.GetPlayerInput(playerNumberToGiveStat).GetComponent<PlayerController>();
        upgradeCardScript = upgradeController.chosenUpgradeCard.GetComponent<UpgradeCardScript>();

        switch (upgradeCardScript.currentCardType)
        {
            case UpgradeCardScript.CardType.HealthUpButBiggerPlayer:
                IncreaseHealth(upgradeCardScript.maxHealthModifier);
                ChangePlayerScale(upgradeCardScript.sizeIncreaseModifier);
                break;
            case UpgradeCardScript.CardType.HealthUpButSlowerPlayerSpeed:
                IncreaseHealth(upgradeCardScript.maxHealthModifier);
                ChangePlayerSpeed(-upgradeCardScript.moveSpeedModifier);
                break;
            case UpgradeCardScript.CardType.SpeedUp:
                ChangePlayerSpeed(upgradeCardScript.moveSpeedModifier);
                break;
            case UpgradeCardScript.CardType.DamageUpButSlowerBulletSpeed:
                ChangeBulletDamage(upgradeCardScript.bulletDamageModifier);
                ChangeBulletSpeed(-upgradeCardScript.moveSpeedModifier * 1.75f);
                break;
            case UpgradeCardScript.CardType.FireRateUpButSmallerBullets:
                ChangeFireRate(upgradeCardScript.fireRateModifier);
                ChangeBulletScale(upgradeCardScript.sizeDecreaseModifier);
                break;
            case UpgradeCardScript.CardType.BulletSpeedUpButSlowerFireRate:
                ChangeBulletSpeed(upgradeCardScript.moveSpeedModifier * 1.75f);
                ChangeFireRate(-upgradeCardScript.fireRateModifier);
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
    }

    private void ChangePlayerSpeed(float moveSpeed)
    {
        playerStats.moveSpeed += moveSpeed;
    }

    private void ChangeBulletSpeed(float bulletSpeed = 4)
    {
        playerStats.GetComponent<ShootController>().bulletSpeed += bulletSpeed;
    }

    private void ChangeBulletDamage(int bulletDamage = 1)
    {
        playerStats.bulletDamage += bulletDamage;
    }

    private void ChangeBulletScale(float sizeModifier)
    {
        playerStats.bulletSize *= sizeModifier;
    }

    private void ChangePlayerScale(float sizeIncrease)
    {
        playerStats.gameObject.transform.localScale *= sizeIncrease;
    }

    private void IncreaseHealth(int maxHealth)
    {
        playerStats.maxHealth += maxHealth;
    }
}
