using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMap : MonoBehaviour
{

    public Sprite[] sprites;
    public GameObject[] wallPrefabs;

    private int randomPrefab;
    private GameObject currentWalls;
    public GameObject duckWall;
    public Vector3 duckSpot;
    //public static bool duckWallOn = false;
    public GameObject duckObj = null;

    public void RandomizeMap()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        randomPrefab = Random.Range(0, wallPrefabs.Length);

        if (randomPrefab == 11)
        {
            //var duckPos = new Vector3(duckSpot.x, duckSpot.y, duckSpot.z);

            //duckWallOn = true;
            duckObj = Instantiate(duckWall, duckSpot, Quaternion.identity);
        }
        currentWalls = Instantiate(wallPrefabs[randomPrefab]);
    }

    public void ClearMap()
    {
        if (duckObj != null)
        {
            Destroy(duckObj);
        }

        if (currentWalls != null)
        {
            Destroy(currentWalls);
        }
    }
}
