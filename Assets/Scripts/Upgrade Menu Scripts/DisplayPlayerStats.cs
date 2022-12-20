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

    private PlayerController playerStats;

    [Header("Score Components")]
    public int playerNumberDisplayBelongsTo;
    public int currentScore;

    public Image scoreCrown1;
    public Image scoreCrown2;
    public Image scoreCrown3;

    public Sprite crownEmpty;
    public Sprite crownPoint;

    void Start()
    {

    }

    void Update()
    {

    }

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


        if (GameManager.Instance.GetPlayerInput(playerNumberDisplayBelongsTo - 1) != null)
        {
            UpdateStatScreen(playerNumberDisplayBelongsTo);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
}