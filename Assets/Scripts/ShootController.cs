using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public int playerNumber = 1;
    public GameObject bulletPrefab;

    public float bulletSpeed;

    public Transform Spawnpoint;

    void Update()
    {
        if (Input.GetButtonDown($"Fir{playerNumber}"))
        {
            var newBullet = Instantiate(bulletPrefab, Spawnpoint.position, Spawnpoint.rotation);
            newBullet.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
            Destroy(newBullet, 2);
        }
    }
}
