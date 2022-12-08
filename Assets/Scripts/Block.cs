using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Collider2D col;
    public SpriteRenderer rend;

    public float blockTimeup;
    public float blockCooldown;
    public bool canBlock;

    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        //col = GetComponent<Collider2D>();
        //rend = GetComponent<SpriteRenderer>();
        
        playerController = gameObject.GetComponent<PlayerController>();

        col.enabled = false;
        rend.enabled = false;

        canBlock = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnBlock()
    {
        if(canBlock == true)
        {
            StartCoroutine(blocking());
        }
    }

    private IEnumerator blocking()
    {
        canBlock = false;
        col.enabled = true;
        rend.enabled = true;
        playerController.isBlocking = true;
        yield return new WaitForSeconds(blockTimeup);

        col.enabled = false;
        rend.enabled = false;
        playerController.isBlocking = false;
        yield return new WaitForSeconds(blockCooldown);

        canBlock = true;

    }
}
