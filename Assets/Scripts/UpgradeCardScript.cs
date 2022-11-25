using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeCardScript : MonoBehaviour
{
    private int amountOfStats = 4;
    private int randomNumber;
    private int oldRandomNumber;

    public float statModifier = 2;

    public enum CardType
    {
        DamageUp,
        HealthUp,
        BulletSize,
        SpeedUp,

    }

    // Start is called before the first frame update
    void Start()
    {
        randomNumber = Random.Range(0, 100);
        //Debug.Log(randomNumber);
        if (randomNumber <= 80)
        {
            StatCard();
        }

        if (randomNumber > 80)
        {
            //Instantiate() rare card here
            //Debug.Log("Rare Card Acquired!");

            this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "SPECIAL \n CARD";
        }
    }

    private void StatCard()
    {
        int chosenStat = Random.Range(0, amountOfStats);

        switch (chosenStat)
        {
            case 1:
                //CardType.BulletSize.
                Debug.Log("Given damage card");
                this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "DMG";
                break;

            case 2:
                Debug.Log("Given health card");
                this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "HEALTH";
                break;

            case 3:
                Debug.Log("Given bulletSize card");
                this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "BULLET\n" +
                    "SIZE";
                break;

            case 4:
                Debug.Log("Given speed card");
                this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "SPEED";
                break;
        }
    }

    
}
