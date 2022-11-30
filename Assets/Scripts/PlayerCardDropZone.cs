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

    public float speed = 100;

    // Start is called before the first frame update
    void Start()
    {
        AddSelfToTags();
    }

    private void AddSelfToTags()
    {
        if (gameObject.name.Contains("Player1"))
        {
            tagsOfCardsPlaced.Add("DraggablePlayer1");
        }

        if (gameObject.name.Contains("Player2"))
        {
            tagsOfCardsPlaced.Add("DraggablePlayer2");
        }

        if (gameObject.name.Contains("Player3"))
        {
            tagsOfCardsPlaced.Add("DraggablePlayer3");
        }

        if (gameObject.name.Contains("Player4"))
        {
            tagsOfCardsPlaced.Add("DraggablePlayer4");
        }
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

                //CardToPosition.transform.position = Vector3.MoveTowards(CardToPosition.transform.position, slotsToPlace[i].transform.position, speed * Time.deltaTime);
                upgradeCardScript = CardToPosition.GetComponent<UpgradeCardScript>();
                tagsOfCardsPlaced.Add(CardToPosition.tag);
                slotsUsed++;
                CardToPosition.GetComponent<Collider2D>().enabled = false;
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
