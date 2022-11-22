using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeScript : MonoBehaviour
{
    [Header("Upgrades")]


    [Space]


    public int numberOfCards = 5;
    public Transform cardSpawnSpot;
    public bool doUpgrade;
    public GameObject card;

    void Start()
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            //var UpgradeCardObj = Instantiate(card, cardSpawnSpot.position + new Vector3(4 * i, 0, 0), Quaternion.identity);
            //UpgradeCardObj.transform.parent = gameObject.transform;
        }
    }

    void Update()
    {

    }

    public void HealthUpgrade(int healthMod)
    {

    }
}
