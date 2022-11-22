using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject PlayerPrefab1;
    public GameObject PlayerPrefab2;
    public GameObject PlayerPrefab3;
    public GameObject PlayerPrefab4;

    public Transform Spawnpoint1;
    public Transform Spawnpoint2;
    public Transform Spawnpoint3;
    public Transform Spawnpoint4;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(PlayerPrefab1, Spawnpoint1.position, Spawnpoint1.rotation);
        Instantiate(PlayerPrefab2, Spawnpoint2.position, Spawnpoint2.rotation);
        Instantiate(PlayerPrefab3, Spawnpoint3.position, Spawnpoint3.rotation);
        Instantiate(PlayerPrefab4, Spawnpoint4.position, Spawnpoint4.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
