using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayPlayerStatsDEPRECATED : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI dmgText;
    public TextMeshProUGUI firerateText;
    public TextMeshProUGUI speedText;
    public int playerNumberDisplayBelongsTo;
    public int currentScore;

    public float hphToAdd;
    public float dmgToAdd;
    public float firerateToAdd;
    public float speedToAdd;

    private PlayerController playerStats;
    public Animator anim;

    public Image scoreCrown1;
    public Image scoreCrown2;
    public Image scoreCrown3;

    public Sprite crownEmpty;
    public Sprite crownPoint;

    public GameManager gameManager;

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
            hpText.text = $"{hpText.text}+<color=#73ad41>{hphToAdd}</color>";
        }

        if (dmgToAdd != 0)
        {
            ResetStatScreen(dmgText);
            dmgText.text = $"{dmgText.text}+<color=#73ad41>{dmgToAdd}</color>";
        }

        if (firerateToAdd > 0)
        {
            ResetStatScreen(firerateText);
            firerateText.text = $"{firerateText.text}+<color=#73ad41>{firerateToAdd}</color>";
        }
        else if (firerateToAdd < 0)
        {
            ResetStatScreen(firerateText);
            firerateText.text = $"{firerateText.text}-<color=red>{-firerateToAdd}</color>";
        }

        if (speedToAdd > 0)
        {
            ResetStatScreen(speedText);
            speedText.text = $"{speedText.text}+<color=#73ad41>{speedToAdd}</color>";
        }
        else if (speedToAdd < 0)
        {
            ResetStatScreen(speedText);
            speedText.text = $"{speedText.text}-<color=red>{-speedToAdd}</color>";
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

    public void OnEnable() // its in update. i dont care, fight me about it why dont'chu
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
    }
}
