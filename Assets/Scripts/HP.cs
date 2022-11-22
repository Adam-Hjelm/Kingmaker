using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public Image healthBar;

    public int currentHealth;
    public int maxHealth;

    void Start()
    {
        healthBar.fillAmount = maxHealth;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(25);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GainHP(25);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    public void GainHP(int HP)
    {
        currentHealth += HP;
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }
}
