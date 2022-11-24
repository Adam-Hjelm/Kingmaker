using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public int maxHealth;
    public float bulletDamage;
    public float fireRate = 200;



    //public static PlayerController[] playerInstances;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerController[] playerInstances = GameObject.FindObjectsOfType<PlayerController>();

        if (playerInstances.Length > 4)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
