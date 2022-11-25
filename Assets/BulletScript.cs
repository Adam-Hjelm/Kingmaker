using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed;


    void Start()
    {
        Destroy(gameObject, 2);
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
    }

    void Update()
    {
    }
}
