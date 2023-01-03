using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject meleeHitBox;
    public Transform spawnPoint;
    public Transform centralPoint;
    public float bulletSpeed;
    public int healingBulletsAmount;

    PlayerController playerController;
    public bool canShoot = true;
    public float timer;
    public float startTimer = 1f;

    public RuntimeAnimatorController healingBulletAnim;

    public Animator handAnim;
    public AudioSource source;
    public AudioClip fireballSound;
    public MeleeHit meleeHit;

    private int fireballShotCounter;

    //public GameObject SmokePrefab;
    //public Transform SmokePoint;


    private void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        handAnim = spawnPoint.GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        meleeHit = spawnPoint.GetComponent<MeleeHit>();
    }

    private void OnEnable()
    {
        spawnPoint.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnDisable()
    {
        spawnPoint.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        timer -= Time.deltaTime;


    }




    private void ActivateMelee()
    {
        Debug.Log("punched");
        var handCollider = spawnPoint.GetComponent<Collider2D>();

        spawnPoint.gameObject.tag = "meleeHitBox";
        Physics2D.IgnoreCollision(handCollider, GetComponent<Collider2D>());

        meleeHit.meleeDamage = playerController.bulletDamage;
        handAnim.SetTrigger("Punching");
    }

    public void DeactivateMelee()
    {
        spawnPoint.gameObject.tag = "Untagged";
    }

    void OnFire()
    {
        if (meleeHit != null)
        {

            if (!meleeHit.meleeRange)
            {
                if (enabled && canShoot == true && playerController.isBlocking == false && timer <= playerController.fireRate)
                {
                    handAnim.SetTrigger("Casting");

                    if (playerController.bulletSpread > 0)
                    {
                        float facingRotation = centralPoint.rotation.eulerAngles.z + 90;
                        float startRotation = facingRotation + playerController.bulletSpread / 2f;
                        float angleIncrease = playerController.bulletSpread / ((float)playerController.bulletAmount - 1f);

                        for (int i = 0; i < playerController.bulletAmount; i++)
                        {
                            InstantiateBullet(startRotation - angleIncrease * i);
                        }
                    }
                    else
                    {
                        InstantiateBullet(centralPoint.rotation.eulerAngles.z + 90);
                    }

                    //GameObject newSmoke = Instantiate(SmokePrefab, SmokePoint.position, SmokePoint.rotation);
                    //Destroy(newSmoke, 0.2f);

                    timer = startTimer;
                }
            }
            else
            {
                ActivateMelee();
                timer = startTimer;
            }
        }
    }


    private void InstantiateBullet(float bulletRotation)
    {
        GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.Euler(0f, 0f, (bulletRotation - 90)));
        Vector2 moveDirection = new Vector2(Mathf.Cos(bulletRotation * Mathf.Deg2Rad), Mathf.Sin(bulletRotation * Mathf.Deg2Rad));
        newBullet.GetComponent<Rigidbody2D>().velocity = moveDirection * bulletSpeed;
        Vector3 scaleChange = new Vector3(playerController.bulletSize.x, playerController.bulletSize.y, playerController.bulletSize.z);

        if (playerController.bulletsRandomSized != true)
        {
            newBullet.transform.localScale = scaleChange;
        }
        else
        {
            float randomizedScale = UnityEngine.Random.Range(0.4f, 1.5f + playerController.bulletSize.x);
            newBullet.transform.localScale = new Vector3(randomizedScale, randomizedScale, randomizedScale);
        }

        if (newBullet.transform.localScale.magnitude >= Vector3.one.magnitude * 4)
            newBullet.transform.localScale = Vector3.one * 4;
        else if (newBullet.transform.localScale.magnitude <= Vector3.one.magnitude * 0.2f)
            newBullet.transform.localScale = Vector3.one * 0.2f;

        Physics2D.IgnoreCollision(newBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(newBullet.GetComponent<Collider2D>(), spawnPoint.GetComponent<Collider2D>());
        newBullet.GetComponent<BulletScript>().bulletDamage = playerController.bulletDamage;

        if (playerController.healingBullets == true)
        {
            fireballShotCounter++;
            if (fireballShotCounter >= 5 - healingBulletsAmount)
            {
                newBullet.GetComponent<BulletScript>().bulletDamage = -playerController.bulletDamage;
                newBullet.GetComponent<Animator>().runtimeAnimatorController = healingBulletAnim;
                newBullet.GetComponent<BulletScript>().healingBullet = true;
                fireballShotCounter = 0;
            }
        }

        FireBallSound();
    }

    private void FireBallSound()
    {
        source.PlayOneShot(fireballSound);
    }
}
