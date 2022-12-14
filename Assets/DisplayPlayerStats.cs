using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPlayerStats : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI dmgText;
    public TextMeshProUGUI firerateText;
    public TextMeshProUGUI speedText;

    public float hphToAdd;
    public float dmgToAdd;
    public float firerateToAdd;
    public float speedToAdd;

    private PlayerController playerStats;
    public Animator anim;

    private void Start()
    {
        playerStats = GameManager.Instance.GetPlayerInput(1).GetComponent<PlayerController>();

        hpText.text = playerStats.maxHealth.ToString();
        dmgText.text = playerStats.bulletDamage.ToString();
        firerateText.text = playerStats.fireRate.ToString();
        speedText.text = playerStats.moveSpeed.ToString();
    }

    public void UpdateStatScreen(int playerNumberToUpdate)
    {
        playerStats = GameManager.Instance.GetPlayerInput(playerNumberToUpdate).GetComponent<PlayerController>();

        if (hphToAdd != 0)
        {
            hpText.text = $"{hpText.text} +  <color=green>{hphToAdd}</color>";
        }

        if (dmgToAdd != 0)
        {
            dmgText.text = $"{dmgText.text} +  <color=green>{dmgToAdd}</color>";
        }

        if (firerateToAdd > 0)
        {
            firerateText.text = $"{firerateText.text} +  <color=green>{firerateToAdd}</color>";
        }
        else if (firerateToAdd < 0)
        {
            firerateText.text = $"{firerateText.text} -  <color=red>{-firerateToAdd}</color>";
        }

        if (speedToAdd > 0)
        {
            speedText.text = $"{speedText.text} + <color=green>{speedToAdd}</color>";
        }
        else if (speedToAdd < 0)
        {
            speedText.text = $"{speedText.text} - <color=red>{-speedToAdd}</color>";
        }
    }

    public void CleanupStatScreen()
    {
        hpText.text = (playerStats.maxHealth).ToString();
        dmgText.text = (playerStats.bulletDamage).ToString();
        firerateText.text = (playerStats.fireRate).ToString();
        speedText.text = (playerStats.moveSpeed).ToString();

        hphToAdd = 0;
        dmgToAdd = 0;
        firerateToAdd = 0;
        speedToAdd = 0;
    }
}
