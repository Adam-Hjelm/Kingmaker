using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class PlayerController : MonoBehaviour
{
    public int playerNumber = 0;

    [Header("Stats")]
    public float moveSpeed = 3.5f;
    public int maxHealth = 100;
    public int bulletDamage = 1;
    public Vector3 bulletSize;
    public float fireRate = 0.7f;
    public int currentHealth = 100;
    public int bulletAmount;
    public int bulletSpread;

    [Header("Materials")]
    public Material flashMaterial;
    public Material defaultMaterial;

    [Header("Flash")]
    public float flashDuration = 0.1f;
    private Coroutine flashRoutine;


    //[SerializeField] GameObject playerHealthBar;
    [SerializeField] Image healthBar;
    [SerializeField] Image healthBarBackdrop;
    public float healthBarDegradeModifier;

    public SpriteRenderer spriteRenderer;

    CameraShake shake;
    //public DragDropScript dragDropPlayer;

    public AudioClip Explosion;
    public AudioSource Source;
    public UpgradeController upgradeController;
    public GameObject DeathPrefab;
    public Transform DeathAnimationPoint;
    public SpriteRenderer shadowSprite;

    public bool dashing;
    public bool isBlocking;

    EscMenu escMenu;


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

        shadowSprite.enabled = true;

        //healthBar = GameObject.FindWithTag($"Healthbar").GetComponent<Image>();
        //healthBarBackdrop = transform.Find($"HealthbarBackdrop").GetComponent<Image>();
        //healthBar.GetComponentInParent<TrackTarget>().target = gameObject.transform;

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

        if (healthBarBackdrop.fillAmount == 0)
        {
            healthBarBackdrop.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            healthBarBackdrop.transform.parent.gameObject.SetActive(true);
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
            shadowSprite.enabled = false;
            //healthBarBackdrop.fillAmount = healthBar.fillAmount;
            GameObject newDeath = Instantiate(DeathPrefab, DeathAnimationPoint.position, DeathAnimationPoint.rotation);
            Destroy(newDeath, 1);
            GameManager.Instance.KillPlayer(gameObject);
            //ExplosionSound();
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

    //public void OnMoveCursor(InputValue input)
    //{
    //    dragDropPlayer.OnMove(input.Get<Vector2>());
    //}

    //public void OnFireCursor()
    //{
    //    dragDropPlayer.OnFire();
    //}

    private void ExplosionSound()
    {
        Source.PlayOneShot(Explosion);
    }

    void OnEscape()
    {
        escMenu.OnEscape();
    }
}
