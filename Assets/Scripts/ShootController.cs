using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public int playerNumber = 1;
    public GameObject bulletPrefab;


    public Transform Spawnpoint;

    public PlayerController playerController;
    private float timer;
    private float startTimer = 10f;


    private void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        //Debug.Log(timer);
        if (Input.GetButton($"Fir{playerNumber}") && timer <= playerController.fireRate)
        {
            //dostuff
            Debug.Log("SHOT");
            Instantiate(bulletPrefab, Spawnpoint.position, Spawnpoint.rotation);

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
