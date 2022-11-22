using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float bulletSpeed;

    public Transform Spawnpoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var newBullet = Instantiate(bulletPrefab, Spawnpoint.position, Spawnpoint.rotation);
            newBullet.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
            Destroy(newBullet, 2);
        }

        Vector2 MousePos = Input.mousePosition;

        MousePos = Camera.main.ScreenToWorldPoint(MousePos);

        transform.up = (Vector3)MousePos - transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
