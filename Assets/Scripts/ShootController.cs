using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootController : MonoBehaviour
{
    //public int playerNumber = 1;
    public GameObject bulletPrefab;

    public Transform Spawnpoint;

    private bool canShoot = true;

    PlayerController playerController;
    private float timer;
    private float startTimer = 1f;
    public Animator handAnim;


    private void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        handAnim = Spawnpoint.GetComponent<Animator>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        //Debug.Log(timer);
        

        if (gameObject.GetComponent<SpriteRenderer>().enabled == false)
        {
            canShoot = false;
            Spawnpoint.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            canShoot = true;
            Spawnpoint.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        // Vector2 MousePos = Input.mousePosition;
        // MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        // transform.up = (Vector3)MousePos - transform.position;
    }

    void OnFire()
    {
        if (timer <= playerController.fireRate && canShoot == true)
        {
            handAnim.SetTrigger("Casting");
            //dostuff
            GameObject newBullet = Instantiate(bulletPrefab, Spawnpoint.position, Spawnpoint.rotation);
            Physics2D.IgnoreCollision(newBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            newBullet.GetComponent<BulletScript>().bulletDamage = playerController.bulletDamage;

            timer = startTimer;
        }
    }
}
