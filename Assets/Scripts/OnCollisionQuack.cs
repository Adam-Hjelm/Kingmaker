using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionQuack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bullet"))
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
