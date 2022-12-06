using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed;
    public int bulletDamage = 25;

    public GameObject explosionPrefab;
    public Transform explosionPoint;

    //private string[] allPlayerTags = { "Player1", "Player2", "Player3", "Player4" };


    void Start()
    {
        Destroy(gameObject, 2);
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Player3") || other.CompareTag("Player4"))
        {
            if (other.GetComponent<PlayerController>().dashing == false)
            {

                GameObject newExplosion = Instantiate(explosionPrefab, explosionPoint.position, explosionPoint.rotation);
                Destroy(newExplosion, 0.5f);
                Destroy(gameObject);
            }

        }

        if (other.CompareTag("Environment"))
        {
            GameObject newExplosion = Instantiate(explosionPrefab, explosionPoint.position, explosionPoint.rotation);
            Destroy(newExplosion, 0.5f);
            Destroy(gameObject);
        }

    }
}
