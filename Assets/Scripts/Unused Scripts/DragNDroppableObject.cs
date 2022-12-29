using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDroppableObject : MonoBehaviour
{
    public PlayerController playerStats;

    public PlayerCardDropZone playerCardDropZone;
    public DragDropScript dragDropScript;

    [Header("Stats In Card")]
    public float moveSpeed;
    public int maxHealth;
    public float bulletDamage;
    public float fireRate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.parent == null && other.gameObject.CompareTag("DropZone"))
        {
            playerCardDropZone = other.gameObject.GetComponent<PlayerCardDropZone>();

            StartCoroutine(CallFunctions());
            playerCardDropZone.PositionCard(gameObject);
            dragDropScript.colliding = false;
        }
    }

    IEnumerator CallFunctions() // This is for a bugfix
    {
        yield return new WaitForSeconds(0.075f);
        playerCardDropZone.PositionCard(gameObject);
        dragDropScript.colliding = false;
    }

    private void Update()
    {
        if (transform.parent != null && transform.parent.GetComponent<DragDropScript>() == true)
        {
            dragDropScript = transform.parent.GetComponent<DragDropScript>();
        }
    }
}
