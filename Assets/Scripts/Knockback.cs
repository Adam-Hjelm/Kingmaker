using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;

    public AudioClip OnHit;
    public AudioSource Source;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            Rigidbody2D player = this.GetComponent<Rigidbody2D>();

            if (player != null && player.gameObject.activeInHierarchy)
            {
                player.isKinematic = false;
                Vector2 difference = other.gameObject.GetComponent<Rigidbody2D>().velocity;
                difference = difference.normalized * thrust;
                player.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockBack(player));
                PlayOnHit();
            }
        }
    }
    private IEnumerator KnockBack(Rigidbody2D player)
    {
        if(player != null)
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
