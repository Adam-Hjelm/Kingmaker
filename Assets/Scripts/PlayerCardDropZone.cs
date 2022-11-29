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

        for (int i = 0; i < slotsToPlace.Length; i++) // TODO: Check if this actually works when you have multiple player inputs for submit (aka when the new inputmanager is done)
        {
            //Debug.Log("forloop enter");
            if (i >= slotsUsed)
            {
                CardToPosition.transform.position = slotsToPlace[i].transform.position;
                CardToPosition.GetComponent<Collider2D>().enabled = false;
                tagsOfCardsPlaced.Add(CardToPosition.tag);
                return;
            }
            slotsUsed++;
        }

        UpgradePlayerStats();
    }

    private void UpgradePlayerStats()
    {
        if (upgradeCardScript.currentCardType == UpgradeCardScript.CardType.HealthUp)
        {

        }
        
    }
}
