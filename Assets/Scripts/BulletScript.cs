using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BulletScript : MonoBehaviour
{
    public int bulletDamage = 25;

    public GameObject explosionPrefab;
    public Transform explosionPoint;

    public AudioSource Source;
    public AudioClip explosion;

    //Rigidbody2D rb2d;
    //Vector3 LastVelocity;

    void Start()
    {
        Destroy(gameObject, 2);
        //gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
        Source = GetComponent<AudioSource>();

        //rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //LastVelocity = rb2d.velocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Player3") || other.CompareTag("Player4"))
        {
            if (other.GetComponent<PlayerController>().dashing == false)
            {
                GameObject newExplosion = Instantiate(explosionPrefab, explosionPoint.position, explosionPoint.rotation);
                newExplosion.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                Destroy(newExplosion, 0.4f);
                Destroy(gameObject);
            }

        }

        if (other.CompareTag("Environment") || other.CompareTag("Shield"))
        {
            GameObject newExplosion = Instantiate(explosionPrefab, explosionPoint.position, explosionPoint.rotation);
            Explosion();
            Destroy(newExplosion, 0.4f);
            Destroy(gameObject);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
        //var speed = LastVelocity.magnitude;
        //var direction = Vector3.Reflect(LastVelocity.normalized, collision.contacts[0].normal);
        //rb2d.velocity = direction * Mathf.Max(speed, 0f);
    //}

    private void Explosion()
    {
        //Debug.Log("explodedddd");
        Source.PlayOneShot(explosion);
    }
}
