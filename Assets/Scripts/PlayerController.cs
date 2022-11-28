using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int playerNumber = 1;

    [Header("Stats")]
    public float moveSpeed = 3.5f;
    public int maxHealth = 100;
    public float bulletDamage = 25;
    public float fireRate = 0.7f;
    public int currentHealth = 100;

    [SerializeField] Image Healthbar;


    void Awake()
    {
        PlayerController[] playerInstances = FindObjectsOfType<PlayerController>();

        if (playerInstances.Length > 4)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        currentHealth = maxHealth /*+ addedHealth*/;
    }

    void Start()
    {
        GameManager.Instance.AddPlayer(playerNumber, gameObject, this);
    }

    private void PlayerTakeDmg(int dmg)
    {
        Debug.Log("take damage");
        currentHealth -= dmg;
        Healthbar.fillAmount = (float)currentHealth / (float)maxHealth;

        if (currentHealth <= 0)
        {
            GameManager.Instance.KillPlayer(playerNumber);
        }
    }

    private void PlayerHeal(int healing)
    {
        currentHealth += healing;
        Healthbar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            Destroy(other.gameObject);
            PlayerTakeDmg(25);
        }
    }
}
