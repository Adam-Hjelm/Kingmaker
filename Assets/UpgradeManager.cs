using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public PlayerCardDropZone[] playerCardDropZones;
    public int playersDone;
    public GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void CheckIfPlayerDone()
    {
        for (int i = 0; i < playerCardDropZones.Length; i++)
        {
            if (playerCardDropZones[i].slotsUsed == playerCardDropZones[i].maxSlots)
            {
                playersDone++;
            }
        }

        if (playersDone != 4)
        {
            playersDone = 0;
        }
        else if (playersDone == 4)
        {
            gameManager.Invoke("FinishedUpgrade", 3);
            Destroy(gameObject, 3);
        }
    }
}
