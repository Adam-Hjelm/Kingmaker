using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;

<<<<<<< Updated upstream
    //public GameObject explosionPrefab;
    //public Transform explosionPoint;

    public AudioClip OnHit;
    public AudioSource Source;
=======
    public AudioSource Source;
    public AudioClip Explosion;
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream

                //GameObject newExplosion = Instantiate(explosionPrefab, explosionPoint.position, explosionPoint.rotation);
                //Destroy(newExplosion, 0.8f);
                PlayOnHit();
=======
                ExplosionSound();
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
    private void PlayOnHit()
    {
        Source.PlayOneShot(OnHit);
=======
    private void ExplosionSound()
    {
        Source.PlayOneShot(Explosion);
>>>>>>> Stashed changes
    }
}
