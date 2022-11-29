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

    public void PositionCard(GameObject CardObject)
    {


        for (int j = 0; j < tagsOfCardsPlaced.Count; j++)
        {
            if (CardObject.tag == tagsOfCardsPlaced[j])
            {
                return;
            }
        }

        for (int i = 0; i < slotsToPlace.Length; i++) // TODO: Check if this actually works when you have multiple player inputs for submit (aka when the new inputmanager is done)
        {
            Debug.Log("forloop enter");
            if (i >= slotsUsed)
            {
                CardObject.transform.position = slotsToPlace[i].transform.position;
                CardObject.GetComponent<Collider2D>().enabled = false;
                tagsOfCardsPlaced.Add(CardObject.tag);
                return;
            }
            slotsUsed++;
        }
        Debug.Log("before upgrade");
        UpgradePlayerStats();
    }

    private void UpgradePlayerStats()
    {
        Debug.Log("UPGRADING?");
        playerStats = GameObject.FindGameObjectWithTag($"Player{playerNumberToGiveStat}").GetComponent<PlayerController>();
        Debug.Log("UPGRADING");
        if (upgradeCardScript.currentCardType == UpgradeCardScript.CardType.HealthUp)
        {
            playerStats.maxHealth += 100;
        }

        if (upgradeCardScript.currentCardType == UpgradeCardScript.CardType.DamageUp)
        {
            playerStats.bulletDamage += 25;
        }

        if (upgradeCardScript.currentCardType == UpgradeCardScript.CardType.HealthUp)
        {
            playerStats.moveSpeed += 2;
        }

        if (upgradeCardScript.currentCardType == UpgradeCardScript.CardType.FireRateUp)
        {
            playerStats.fireRate += 0.15f;
        }
    }
}
