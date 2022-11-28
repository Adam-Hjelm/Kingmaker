using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDroppableObject : MonoBehaviour
{
    public PlayerCardDropZone playerDropZone;

    [Header("Stats In Card")]
    public float moveSpeed;
    public int maxHealth;
    public float bulletDamage;
    public float fireRate;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.parent == null && other.gameObject.CompareTag("PlayerZone"))
        {
            playerDropZone = other.GetComponent<PlayerCardDropZone>();
        }
    }
}
