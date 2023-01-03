using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : MonoBehaviour
{
    public bool meleeRange;
    public int meleeDamage;
    public ShootController shootController;
    public PlayerController playerController;

    private void Start()
    {
        playerController = gameObject.GetComponentInParent<PlayerController>();
        shootController = gameObject.GetComponentInParent<ShootController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), playerController.gameObject.GetComponent<Collider2D>());
        //float meleeDistance = 3f;
        if (other.tag.Contains("Player"))
        {
            meleeRange = true;
        }
        //else
        //{
        //    meleeRange = false;
        //}
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Contains("Player"))
        {
            meleeRange = false;
        }
    }
}