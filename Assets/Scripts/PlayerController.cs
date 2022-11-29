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

    [Header("Materials")]
    public Material flashMaterial;
    private Material originalMaterial;

    [Header("Flash")]
    public float flashDuration = 0.1f;
    private Coroutine flashRoutine;

    [SerializeField] Image Healthbar;

    SpriteRenderer spriteRenderer;


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

        spriteRenderer = GetComponent<SpriteRenderer>();

        originalMaterial = spriteRenderer.material;
    }

    private void PlayerTakeDmg(int dmg)
    {
        Debug.Log("take damage");
        currentHealth -= dmg;
        Healthbar.fillAmount = (float)currentHealth / (float)maxHealth;
        Flash();

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

    public void Flash()
    {
        if(flashRoutine != null)
        {
            StopCoroutine(FlashRoutine());
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.material = originalMaterial;

        flashRoutine = null;
            }
}
