using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class PlayerController : MonoBehaviour
{
    public int playerNumber = 1;

    [Header("Stats")]
    public float moveSpeed = 3.5f;
    public int maxHealth = 100;
    public int bulletDamage = 25;
    public float fireRate = 0.7f;
    public int currentHealth = 100;
    public float healthBarDegradeModifier;

    [Header("Materials")]
    public Material flashMaterial;
    public Material defaultMaterial;

    [Header("Flash")]
    public float flashDuration = 0.1f;
    private Coroutine flashRoutine;

    [SerializeField] Image healthBar;
    [SerializeField] Image healthBarBackdrop;

    public SpriteRenderer spriteRenderer;

    CameraShake shake;


    void Awake()
    {
        PlayerController[] playerInstances = FindObjectsOfType<PlayerController>();

        if (playerInstances.Length > 4)
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);
        currentHealth = maxHealth /*+ addedHealth*/;

    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //originalMaterial = spriteRenderer.material; aja baja dumma bug
        spriteRenderer.material = defaultMaterial;
        shake = Camera.main.GetComponent<CameraShake>();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddPlayer(playerNumber, gameObject, this);
        }
        healthBar = GameObject.FindWithTag($"Player{playerNumber}HealthBar").GetComponent<Image>();
        healthBarBackdrop = GameObject.Find($"p{playerNumber}HealthbarBackdrop").GetComponent<Image>();

        //healthBarBackdrop.fillAmount = 1; 
    }

    private void Update()
    {
        if (healthBarBackdrop != null)
        {
            if (healthBarBackdrop.fillAmount > healthBar.fillAmount)
            {
                healthBarBackdrop.fillAmount -= Time.deltaTime * healthBarDegradeModifier;
            }

            if (healthBarBackdrop.fillAmount < healthBar.fillAmount)
            {
                healthBarBackdrop.fillAmount = healthBar.fillAmount;
            }
        }


    }

    private void PlayerTakeDmg(int dmg)
    {
        Debug.Log("take damage");
        currentHealth -= dmg;
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;

        if (currentHealth <= 0 && GameManager.Instance != null)
        {
            StopCoroutine(flashRoutine);
            spriteRenderer.material = defaultMaterial;
            healthBarBackdrop.fillAmount = healthBar.fillAmount;
            GameManager.Instance.KillPlayer(playerNumber);
            shake.start = true;
        }
    }



    private void PlayerHeal(int healing)
    {
        currentHealth += healing;
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    public void SetPlayerHealthToMax()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            Flash();
            Destroy(other.gameObject);
            PlayerTakeDmg(other.gameObject.GetComponent<BulletScript>().bulletDamage);
        }
    }

    public void Flash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(FlashRoutine());
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.material = defaultMaterial;

        flashRoutine = null;
    }
}
