using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public Transform centralPoint;
    public float bulletSpeed;
    private float spreadOffset;
    public int healingBulletsAmount;

    PlayerController playerController;
    public bool canShoot = true;
    public static bool roundStarted;
    private float timer;
    private float startTimer = 1f;

    public RuntimeAnimatorController healingBulletAnim;

    public Animator handAnim;
    public AudioSource Source;
    public AudioClip Fireball;

    private int fireballShotCounter;

    //public GameObject SmokePrefab;
    //public Transform SmokePoint;


    private void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        handAnim = spawnPoint.GetComponent<Animator>();
        Source = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (gameObject.GetComponent<SpriteRenderer>().enabled == false)
        {
            canShoot = false;
            spawnPoint.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (roundStarted == true)
        {
            canShoot = true;
            spawnPoint.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            canShoot = false;
            spawnPoint.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void OnFire()
    {
        if (playerController.isBlocking == false)
        {
            if (timer <= playerController.fireRate && canShoot == true)
            {
                handAnim.SetTrigger("Casting");

                if (playerController.bulletSpread > 0)
                {
                    float facingRotation = centralPoint.rotation.eulerAngles.z + 90;
                    float startRotation = facingRotation + playerController.bulletSpread / 2f;
                    float angleIncrease = playerController.bulletSpread / ((float)playerController.bulletAmount - 1f);

                    for (int i = 0; i < playerController.bulletAmount; i++)
                    {
                        float tempRot = startRotation - angleIncrease * i;

                        InstantiateBullet(tempRot);
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
        Source.PlayOneShot(Fireball);
    }
}
