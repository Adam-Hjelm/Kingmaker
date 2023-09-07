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
        int randomNumber = Random.Range(0, sprites.Length);
        GetComponent<SpriteRenderer>().sprite = sprites[randomNumber];
        randomPrefab = Random.Range(0, wallPrefabs.Length);

        if (randomNumber == 11)
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
