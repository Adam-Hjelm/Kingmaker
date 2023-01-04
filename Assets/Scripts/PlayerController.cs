using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class PlayerController : MonoBehaviour
{
    [Header("Stats Settings")]
    public float moveSpeed = 3.5f;
    public int maxHealth = 100;
    public int bulletDamage = 1;
    public Vector3 bulletSize;
    public float fireRate = 0.7f;
    public int currentHealth = 100;
    public int bulletAmount = 1;
    public int bulletSpread = 0;

    [Space]
    public bool bulletsRandomSized = false;
    public bool healingBullets = false;

    [Header("Flags")]
    public bool dashing = false;
    public bool isBlocking = false;
    public bool roundOver = false;
    public bool playerWon = false;

    [Header("Materials")]
    public Material flashMaterial;
    public Material defaultMaterial;

    [Header("Flash")]
    public float flashDuration = 0.1f;
    public SpriteRenderer spriteRenderer;

    [Header("Health Bar")]
    [SerializeField] Canvas healthCanvas;
    [SerializeField] Image healthBar;
    [SerializeField] Image healthBarBackdrop;
    [SerializeField] float healthBarDegradeModifier;

    [Header("Other")]
    [SerializeField] AudioClip slapSound;
    [SerializeField] AudioClip healingSound;
    [SerializeField] AudioClip Explosion;
    [SerializeField] AudioSource Source;
    [SerializeField] GameObject DeathPrefab;
    [SerializeField] SpriteRenderer shadowSprite;

    [SerializeField] EscMenu escMenu;
    [SerializeField] SpriteRenderer crownObject;

    Coroutine flashRoutine;
    CameraShake shake;

    BulletScript bulletScript;


    void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        Source = GetComponent<AudioSource>();
        spriteRenderer.material = defaultMaterial;
        shake = Camera.main.GetComponent<CameraShake>();
        shadowSprite.enabled = true;
    }

    private void Update()
    {
        if (healthBarBackdrop != null)
        {
            if (healthBarBackdrop.fillAmount > healthBar.fillAmount)
                healthBarBackdrop.fillAmount -= Time.deltaTime * healthBarDegradeModifier;

            if (healthBarBackdrop.fillAmount < healthBar.fillAmount)
                healthBarBackdrop.fillAmount = healthBar.fillAmount;
        }

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    private void PlayerTakeDmg(int dmg)
    {
        currentHealth -= dmg;
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;

        if (currentHealth <= 0 && GameManager.Instance != null)
        {
            StopCoroutine(flashRoutine);
            spriteRenderer.material = defaultMaterial;
            shadowSprite.enabled = false;
            GameObject newDeath = Instantiate(DeathPrefab, transform.position, transform.rotation);
            newDeath.transform.localScale = transform.localScale;
            GameManager.Instance.KillPlayer(gameObject);
            shake.start = true;
        }
    }

    public void SetPlayerHealthToMax()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
        roundOver = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isBlocking)
        {
            if (other.gameObject.CompareTag("bullet") && !dashing)
            {
                Flash();
                Destroy(other.gameObject);
                PlayerTakeDmg(other.gameObject.GetComponent<BulletScript>().bulletDamage);
            }
        }


    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("meleeHitBox"))
        {
            AudioManager.Instance.PlaySlapSound();
            Flash();
            PlayerTakeDmg(other.gameObject.GetComponent<MeleeHit>().meleeDamage);
            other.gameObject.GetComponentInParent<ShootController>().DeactivateMelee();
            GetComponent<Knockback>().PrepareKnockBack(other);
        }
    }

    public void Flash()
    {
        if (flashRoutine != null)
            StopCoroutine(FlashRoutine());

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.material = defaultMaterial;
        flashRoutine = null;
    }

    public void SetPlayerEnabled(bool enabled)
    {
        //spriteRenderer.enabled = enabled;
        GetComponent<Collider2D>().enabled = enabled;
        GetComponent<PlayerMovement>().enabled = enabled;
        GetComponent<ShootController>().enabled = enabled;
        GetComponent<Block>().enabled = enabled;
        healthCanvas.enabled = enabled;
        crownObject.gameObject.SetActive(enabled);

        SpriteRenderer[] allPlayerSprites = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < allPlayerSprites.Length; i++)
        {
            allPlayerSprites[i].enabled = enabled;
        }

        if (playerWon)
            crownObject.enabled = true;
        else
            crownObject.enabled = false;
    }

    private void ExplosionSound()
    {
        Source.PlayOneShot(Explosion);
    }

    public void OnPause()
    {
        GameManager.Instance.TogglePause(this);
    }
}
