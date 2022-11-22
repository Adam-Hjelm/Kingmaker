using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTwoHealth : MonoBehaviour
{
    public HP playerhealth2 = new HP(100, 100);
    public Image Healthbar;

    public int currentHealth;
    public int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        Healthbar.fillAmount = maxHealth;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerTakeDmg(25);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerHeal(25);
        }

        if(currentHealth <= 0)
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
        Healthbar.fillAmount = (float)currentHealth / (float)maxHealth;
    }
    private void PlayerHeal(int healing)
    {
        currentHealth += healing;
        Healthbar.fillAmount = (float)currentHealth / (float)maxHealth;
    }
}
