using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDroppableObject : MonoBehaviour
{
    public PlayerController playerStats;

    public PlayerCardDropZone playerCardDropZone;

    [Header("Stats In Card")]
    public float moveSpeed;
    public int maxHealth;
    public float bulletDamage;
    public float fireRate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("entered");



        if (transform.parent == null && other.gameObject.CompareTag("DropZone"))
        {
            //Debug.Log("entered right");
            playerCardDropZone = other.gameObject.GetComponent<PlayerCardDropZone>();
            playerCardDropZone.PositionCard(gameObject);

            //        CheckIfAbleToGiveCard();
        }
    }

    //private bool CheckIfAbleToGiveCard()
    //{
    //    if(playerCardDropZone)

    //    return true;
    //}
}
