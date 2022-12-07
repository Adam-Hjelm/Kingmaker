using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    SpriteRenderer SR;
    //public Sprite sprite1;
    //public Sprite sprite2;
    //public Sprite sprite3;
    //public Sprite sprite4;

    Collider2D Col;

    public int timesHit;
    public bool canHit;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        Col = GetComponent<Collider2D>();
        timesHit = 3;
        canHit = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canHit)
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("bullet") && timesHit == 3 && canHit == true)
        {
            SR.color = Color.blue;
            //SR.sprite = sprite1;
            timesHit -= 1;
            StartCoroutine(Hit());
        }
        if (other.CompareTag("bullet") && timesHit == 2 && canHit == true)
        {
            SR.color = Color.red;
            //SR.sprite = sprite2;
            timesHit -= 1;
            StartCoroutine(Hit());
        }
        if (other.CompareTag("bullet") && timesHit == 1 && canHit == true)
        {
            SR.color = Color.green;
            //SR.sprite = sprite3;
            timesHit -= 1;
            StartCoroutine(Hit());
        }
        if (other.CompareTag("bullet") && timesHit == 0 && canHit == true)
        {
            SR.color = Color.black;
            //SR.sprite = sprite4;
            Col.enabled = !Col.enabled;
        }
    }

    private IEnumerator Hit()
    {
        canHit = false;
        yield return new WaitForSeconds(0.1f);
        canHit = true;
    }
}
