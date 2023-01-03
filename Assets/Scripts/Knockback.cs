using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;

    //public GameObject explosionPrefab;
    //public Transform explosionPoint;

    public AudioClip OnHit;
    public AudioSource Source;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("meleeHitBox"))
        {
            PrepareKnockBack(other);
        }
    }

    public void PrepareKnockBack(Collider2D other)
    {
        Rigidbody2D player = this.GetComponent<Rigidbody2D>();

        if (player != null && player.gameObject.activeInHierarchy)
        {
            Debug.Log("Preparin knockback");

            player.isKinematic = false;

            Vector2 difference;
            if (other.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                difference = other.gameObject.GetComponent<Rigidbody2D>().velocity;
            }
            else
            {
                difference = transform.position - other.transform.position;
            }


            if (!other.gameObject.CompareTag("bullet"))
            {
                difference = 10 * thrust * difference.normalized;
                Debug.Log("Preparin melee knockback");
            }
            else
            {
                difference = difference.normalized * thrust;
            }
            player.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(KnockBack(player));
            //other.GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;
            //GameObject newExplosion = Instantiate(explosionPrefab, explosionPoint.position, explosionPoint.rotation);
            //Destroy(newExplosion, 0.8f);
            PlayOnHit();
        }
    }

    public IEnumerator KnockBack(Rigidbody2D player)
    {
        if (player != null)
        {
            yield return new WaitForSeconds(knockTime);
            player.velocity = Vector2.zero;
            //player.isKinematic = true;
        }
    }

    private void PlayOnHit()
    {
        Source.PlayOneShot(OnHit);
    }
}
