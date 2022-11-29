using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardDropZone : MonoBehaviour
{
    public PlayerController playerStats;
    public UpgradeCardScript upgradeCardScript;

    public DragDropScript dragDropScript;
    public GameObject[] slotsToPlace;

    public List<string> tagsOfCardsPlaced = new List<string>();
    public int slotsUsed;
    public int maxSlots;
    public int playerNumberToGiveStat;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PositionCard(GameObject CardToPosition)
    {


        for (int j = 0; j < tagsOfCardsPlaced.Count; j++)
        {
            if (CardToPosition.tag == tagsOfCardsPlaced[j])
            {
                return;
            }
        }

        for (int i = 0; i < slotsToPlace.Length; i++)  // TODO: Fix so players can't give themselves a card
        {
            //Debug.Log("forloop enter");
            if (i >= slotsUsed)
            {
                CardToPosition.transform.position = slotsToPlace[i].transform.position;
                CardToPosition.GetComponent<Collider2D>().enabled = false;
                upgradeCardScript = CardToPosition.GetComponent<UpgradeCardScript>();
                tagsOfCardsPlaced.Add(CardToPosition.tag);
                slotsUsed++;
                UpgradePlayerStats();
                return;
            }
        }

    }

    private void UpgradePlayerStats()
    {
        playerStats = GameObject.FindGameObjectWithTag($"Player{playerNumberToGiveStat}").GetComponent<PlayerController>();
        Debug.Log("upgrading...");
        if (upgradeCardScript.currentCardType == UpgradeCardScript.CardType.HealthUp)
        {
            playerStats.maxHealth += 100;
        }

        if (upgradeCardScript.currentCardType == UpgradeCardScript.CardType.DamageUp)
        {
            playerStats.bulletDamage += 25;
        }

        if (upgradeCardScript.currentCardType == UpgradeCardScript.CardType.SpeedUp)
        {
            playerStats.moveSpeed += 2;
        }

        if (upgradeCardScript.currentCardType == UpgradeCardScript.CardType.FireRateUp)
        {
            playerStats.fireRate += 0.15f;
        }
    }
}
