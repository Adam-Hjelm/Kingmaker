using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed;
    public int bulletDamage = 25;

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
        if (other.CompareTag("Environment"))
        {
            // Do fancy impact n particles stuff
            Destroy(gameObject);
        }
    }
}
