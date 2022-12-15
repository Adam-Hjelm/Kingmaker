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
        string currenthpText;

        currenthpText = hpText.text;

        playerStats = GameManager.Instance.GetPlayerInput(playerNumberToUpdate).GetComponent<PlayerController>();

        if (hphToAdd != 0)
        {
            ResetStatScreen(hpText);
            hpText.text = $"{hpText.text} +  <color=green>{hphToAdd}</color>";
        }

        if (dmgToAdd != 0)
        {
            ResetStatScreen(dmgText);
            dmgText.text = $"{dmgText.text} +  <color=green>{dmgToAdd}</color>";
        }

        if (firerateToAdd > 0)
        {
            ResetStatScreen(firerateText);
            firerateText.text = $"{firerateText.text} +  <color=green>{firerateToAdd}</color>";
        }
        else if (firerateToAdd < 0)
        {
            ResetStatScreen(firerateText);
            firerateText.text = $"{firerateText.text} -  <color=red>{-firerateToAdd}</color>";
        }

        if (speedToAdd > 0)
        {
            ResetStatScreen(speedText);
            speedText.text = $"{speedText.text} + <color=green>{speedToAdd}</color>";
        }
        else if (speedToAdd < 0)
        {
            ResetStatScreen(speedText);
            speedText.text = $"{speedText.text} - <color=red>{-speedToAdd}</color>";
        }
    }

    public void ResetStatScreen(TextMeshProUGUI textToReset)
    {
        string[] x = textToReset.text.Split(' ');
        textToReset.text = x[0];
    }

    public void CleanupStatScreen(int playerToCleanup)
    {
        if (GameManager.Instance.GetPlayerInput(playerToCleanup) != null)
        {
            playerStats = GameManager.Instance.GetPlayerInput(playerToCleanup).GetComponent<PlayerController>();
        }
        else
        {
            return;
        }


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
