using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    SpriteRenderer SR;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public GameObject DestroyEffectPrefab;
    //public Transform explosionPoint;


    Collider2D Col;

    public int timesHit;
    public bool canHit;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        Col = GetComponent<Collider2D>();
        //timesHit = 3;
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
        if (other.CompareTag("bullet") && canHit == true)
        {
            timesHit -= 1;

            if(timesHit == 6)
            {
                SR.sprite = sprite1;
                StartCoroutine(Hit());
            }

            else if (timesHit == 4)
            {
                SR.sprite = sprite2;
                StartCoroutine(Hit());
            }

            else if (timesHit == 2)
            {
                SR.sprite = sprite3;
                StartCoroutine(Hit());
            }
            if (timesHit == 0)
            {
                SR.sprite = sprite4;
                StartCoroutine(Hit());
                Col.enabled = !Col.enabled;
                GameObject newExplosion = Instantiate(DestroyEffectPrefab, transform.position, transform.rotation);
                Destroy(newExplosion, 0.5f);
            }
        }

        //if (other.CompareTag("bullet") && timesHit == 6 && canHit == true)
        //{
        //    SR.color = Color.blue;
        //    SR.sprite = sprite1;
        //    timesHit -= 1;
        //    StartCoroutine(Hit());
        //}
        //if (other.CompareTag("bullet") && timesHit == 4 && canHit == true)
        //{
        //    SR.color = Color.red;
        //    SR.sprite = sprite2;
        //    timesHit -= 1;
        //    StartCoroutine(Hit());
        //}
        //if (other.CompareTag("bullet") && timesHit == 2 && canHit == true)
        //{
        //    SR.color = Color.green;
        //    SR.sprite = sprite3;
        //    timesHit -= 1;
        //    StartCoroutine(Hit());
        //}
        //if (other.CompareTag("bullet") && timesHit == 0 && canHit == true)
        //{
        //    SR.color = Color.black;
        //    SR.sprite = sprite4;
        //    DestroyEffect.SetActive(true);
        //    Col.enabled = !Col.enabled;
        //    GameObject newExplosion = Instantiate(DestroyEffectPrefab, transform.position, transform.rotation);
        //    Destroy(newExplosion, 0.5f);
        //}
    }

    private IEnumerator Hit()
    {
        canHit = false;
        yield return new WaitForSeconds(0.1f);
        canHit = true;
    }
}
