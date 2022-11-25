using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public HP playerhealth = new HP(100, 100);
    public PlayerController playerController;
    public Image Healthbar;

    public int currentHealth;
    //public int maxHealth;
    //public int addedHealth = 0;



    // Start is called before the first frame update
    void Start()
    {
        FindHealthBars();

        playerController = gameObject.GetComponent<PlayerController>();

        currentHealth = playerController.maxHealth /*+ addedHealth*/;
        //Healthbar.fillAmount = playerController.maxHealth;
    }

    private void FindHealthBars()
    {
        if (gameObject.name.Contains("1"))
        {
            // TODO: INSERT FETCHING OF HEALTHBARS AND ADD ONE FOR EACH PLAYER. MAKE IT IN A SMARTER WAY?
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    PlayerTakeDmg(25);
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    PlayerHeal(25);
        //}
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            PlayerTakeDmg(25);
        }
    }

    private void PlayerTakeDmg(int dmg)
    {
        currentHealth -= dmg;
        Healthbar.fillAmount = (float)currentHealth / (float)playerController.maxHealth;
    }
    private void PlayerHeal(int healing)
    {
        currentHealth += healing;
        Healthbar.fillAmount = (float)currentHealth / (float)playerController.maxHealth;
    }
}
