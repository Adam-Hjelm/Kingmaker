using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public int playerNumber = 1;
    public GameObject bulletPrefab;

    public float bulletSpeed;

    public Transform Spawnpoint;

    public PlayerController playerController;
    private float timer;
    private float startTimer;


    private void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        //Debug.Log(timer);
        if (Input.GetButtonDown($"Fir{playerNumber}") && timer <= playerController.fireRate) // TODO: FIX THAT I CANNOT SHOOT
        {
            //dostuff
            Debug.Log("SHOT");
            var newBullet = Instantiate(bulletPrefab, Spawnpoint.position, Spawnpoint.rotation);
            newBullet.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
            Destroy(newBullet, 2);
            timer = startTimer;
        }


        // Vector2 MousePos = Input.mousePosition;
        // MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        // transform.up = (Vector3)MousePos - transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
